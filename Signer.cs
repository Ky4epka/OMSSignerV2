using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using SevenZip;
using System.IO;
using System.Diagnostics;

/*
namespace OMSSigner
{
    
       class Signer
       {
           public static void LogWrapper(string message, LogLevel level)
           {
               Log._(message, level);
           }

           public static void ProvideDirectory(string path)
           {
               if (!Directory.Exists(path))
                   Directory.CreateDirectory(path);
           }

           public static void ClearTempPath()
           {
               try
               {
                   Log._("Очистка временной директории...", LogLevel.Info);
                   if (Directory.Exists(Globals.TEMP_WORKING_DIR))
                       Directory.Delete(Globals.TEMP_WORKING_DIR, true);
               }
               catch (Exception e)
               {
                   Log._(e, "Не удалось очистить входную директорию по причине: ");
               }
           }

           public static string ExtractFileExtWithoutPoint(string FilePath)
           {
               string extension = Path.GetExtension(FilePath);

               if (extension.Length > 0)
               {
                   if (extension[0] == '.')
                       extension = extension.Remove(0, 1);
               }
               else
               {
                   return "";
               }

               return extension;
           }

           public static bool MatchFileExtension(string extension, string[] extensions)
           {
               extension = extension.ToLower();

               foreach (string ext in extensions)
               {
                   if (extension.Equals(ext.ToLower())) return true;
               }

               return false;
           }

           public static void ExecProcess(string exec_file, string []param, int wait_timeout_ms)
           {
               string param_str = "";

               foreach (string p in param)
               {
                   param_str += " " + p;
               }

               Process s = Process.Start(exec_file, param_str);
               s.WaitForExit(wait_timeout_ms);
           }

           public static void MakeTestFile(string fileName)
           {
               File.WriteAllText(fileName, "Test");
           }

           public static void SignFile(string InputSigningFile, string OutputSignFile, string SignProfileName, bool joinSignatures)
           {
               string inputFileName = Path.GetFileName(InputSigningFile);
               string inputFilePath = InputSigningFile;
               string outputFileDir = OutputSignFile;
               string profileName = SignProfileName;
               string opName = "s";
               string signed_file = OutputSignFile + "\\" + inputFileName + ".sig";

               if (File.Exists(signed_file) && joinSignatures)
               {
                   inputFilePath = signed_file;
                   outputFileDir = OutputSignFile;
                   profileName = SignProfileName;
                   opName = "a";
               }

               LogWrapper("Подпись файла \"" + inputFileName + "\" профилем \"" + SignProfileName + "\"", LogLevel.Info);
               ExecProcess(Globals.TDHELPER_SCRIPT, new string[4] {
               "\"" + inputFilePath + "\"",
               "\"" + outputFileDir + "\"",
               "\"" + profileName + "\"",
               opName},
               10000);
           }


           public static bool SignFileEx(string InputSigningFile, string OutputSignDir, string sign_ext)
           {
               string inputFileName = Path.GetFileName(InputSigningFile);
               string inputFileNameNoExt = Path.GetFileNameWithoutExtension(inputFileName); 
               string ext = ExtractFileExtWithoutPoint(InputSigningFile);
               ProgramConfig.HandlerType handler = ProgramConfig.Instance.GetHandlerTypeBySigningFileExt(ext);

               if (handler != null)
               {
                   foreach (string profile in handler.ArmProfiles)
                   {
                       SignFile(InputSigningFile, OutputSignDir, profile, handler.JoinProfiles);
                   }

                   string dest_file = OutputSignDir + "\\" + inputFileName;

                   if (!handler.DeleteFileAfterSign && (!InputSigningFile.ToUpper().Equals(dest_file.ToUpper())))
                       File.Copy(InputSigningFile, dest_file, true);

                   string src_sign_file = OutputSignDir + "\\" + inputFileName + ".sig";
                   if (File.Exists(src_sign_file) && (!sign_ext.ToLower ().Equals(".sig")))
                   {
                       File.Move(src_sign_file, OutputSignDir + "\\" + inputFileName + sign_ext);
                   }

                   return handler.ArmProfiles.Length > 0;
               }
               else
               {
                   File.Copy(InputSigningFile, OutputSignDir + "\\" + inputFileName, true);
               }

               return false;
           }


           public static void SignDirEx(string InputSigningDir, string OutputSignDir, string sign_ext=".sig")
           {
               foreach (string file in Directory.EnumerateFiles(InputSigningDir))
               {
                   try
                   {
                       SignFileEx(file, OutputSignDir, sign_ext);
                   }
                   catch (Exception e)
                   {
                       Log._(e, "Не удалось подписать файл \"" + file + "\" по причине: ");
                   }
               }
           }



           public static void SignOMSPacket(string PacketFile, ProgramConfig config)
           {
               ClearTempPath();
               string packetFileName = Path.GetFileName(PacketFile);
               string packetFileNameNoExt = Path.GetFileNameWithoutExtension(PacketFile);

               Log._("Извлечение файлов из пакета...", LogLevel.Info);

               using (SevenZipExtractor SevenExtr = new SevenZipExtractor(PacketFile))
               {
                   SevenExtr.ExtractArchive(Globals.TEMP_PACKET_SOURCE_DIR);
               }

               Log._("Подписывание файлов...", LogLevel.Info);
               SignDirEx(Globals.TEMP_PACKET_SOURCE_DIR, Globals.TEMP_PACKET_BUILD_DIR, ".sigm");

               Log._("Упаковка файлов в OMS-архив...", LogLevel.Info);
               SevenZipCompressor SevenCompr = new SevenZipCompressor();

               SevenCompr.ArchiveFormat = OutArchiveFormat.Zip;

               string outputPacketFile = Globals.TEMP_OUTPUT_PACKET_BUILD_DIR + "\\" + Path.GetFileName(PacketFile);
               SevenCompr.CompressDirectory(Globals.TEMP_PACKET_BUILD_DIR, outputPacketFile, true);

               Log._("Подпись пакета: " + outputPacketFile, LogLevel.Info);
               try
               {
                   SignFileEx(outputPacketFile, Globals.TEMP_OUTPUT_PACKET_BUILD_DIR, ".sigm");
               }
               catch (Exception e)
               {
                   Log._(e, "Не удалось подписать пакет по причине: ");
               }

               SevenCompr.CompressDirectory(Globals.TEMP_OUTPUT_PACKET_BUILD_DIR, config.OutPath + "\\" + packetFileNameNoExt + Globals.OMS_PACKET_EXT, true);
           }

           public static void Sign(ProgramConfig config, bool empty_info=false)
           {
               if (!Directory.Exists(config.InPath))
               {
                   LogWrapper("Путь ко входной директории не найден: " + config.InPath, LogLevel.Fatal);
                   return;
               }

               if (!Directory.Exists(config.OutPath))
               {
                   Directory.CreateDirectory(config.OutPath);
                   LogWrapper("Создана отсутствующая выходная директория: " + config.OutPath, LogLevel.Info);
               }

               int packet_count = 0;

               foreach (string file in Directory.EnumerateFiles(config.InPath, "*.zip", SearchOption.TopDirectoryOnly))
               {
                   packet_count++;
               }

               if ((packet_count == 0) && (!empty_info))
               {
                   Log._("Обрабатывать нечего...", LogLevel.Info);
               }

               int counter = 0;
               foreach (string file in Directory.EnumerateFiles(config.InPath, "*.zip", SearchOption.TopDirectoryOnly))
               {
                   counter++;
                   string moving_file = config.HandledRepositoryPath + "\\" + Path.GetFileName(file);

                   try
                   {
                       Log._("Начата обработка пакета(" + counter + " из " + packet_count + ")..", LogLevel.Info);
                       SignOMSPacket(file, config);
                       ProvideDirectory(config.HandledRepositoryPath);


                       if (File.Exists(moving_file))
                           File.Delete(moving_file);

                       Log._(@"Обработка пакета завершена..", LogLevel.Info);
                   }
                   catch (Exception e)
                   {
                       Log._(e, "Не удалось обработать пакет \"" + Path.GetFileName(file) + "\" из-за ошибки: ");
                   }
                   finally
                   {
                       try
                       {
                           File.Move(file, moving_file);
                       }
                       catch (Exception e)
                       {
                           Log._(e, "Не удалось переместить пакет \"" + Path.GetFileName(file) + "\" в обработанные по причине: ");
                       }
                   }
               }
           }
       }
   }

   
}




        public static void SignFileForProfiles(string InputSigningFile, string OutputSignFile, string[] SignProfiles)
        {
            foreach (string profile in SignProfiles)
            {
                SignFile(InputSigningFile, OutputSignFile, profile, true);
            }
        }


        public static void SignDir(string InputSigningDir, string[] extensions, string OutputSignDir, string SignProfileName)
        {
            foreach (string file in Directory.EnumerateFiles(InputSigningDir))
            {
                string ext = ExtractFileExtWithoutPoint(file);
                string fileName = Path.GetFileName(file);

                if (MatchFileExtension(ext, extensions))
                {
                    SignFile(file, OutputSignDir, SignProfileName, true);
                }
            }
        }

        public static void SignDirForProfiles(string InputSigningDir, string[] extensions, string OutputSignDir, string[] SignProfiles)
        {
            foreach (string profile in SignProfiles)
            {
                try
                {
                    SignDir(InputSigningDir, extensions, OutputSignDir, profile);
                }
                catch (Exception e)
                {
                    Log._(e, "Ошибка работы с профилем: " + profile);
                }
            }
        }
 */
 