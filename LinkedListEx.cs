using System.Collections;
using System.Collections.Generic;

public class LinkedListEx<T> : LinkedList<T>
{
    public System.Comparison<T> SortComparer = null;

    public void AddSorted(LinkedListNode<T> node, System.Comparison<T> comparison)
    {
        LinkedListNode<T> current = First;

        if (current == null)
        {
            AddLast(node);
            return;
        }

        while (current != null)
        {
            LinkedListNode<T> next = current?.Next;
            int left_cmp = System.Math.Sign(comparison(node.Value, current.Value));
            int right_cmp = 0;

            if (next != null)
                right_cmp = System.Math.Sign(comparison(node.Value, next.Value));

            if ((current == First) && ((left_cmp == -1) && (right_cmp == -1)))
            {
                AddBefore(current, node);
                break;
            }
            else if (((left_cmp >= 0) && (right_cmp < 0)) || (next == null))
            {
                if (left_cmp >= 0)
                    AddAfter(current, node);
                else
                    AddBefore(current, node);
                break;
            }

            current = next;
        }
    }

    public void AddSorted(T value, System.Comparison<T> comparison)
    {
        AddSorted(new LinkedListNode<T>(value), comparison);
    }

    public void AddSorted(T value)
    {
        if (SortComparer == null)
            throw new System.NullReferenceException("Can not add sorted item for reason: Delegate field 'SortComparer' is null");

        AddSorted(value, SortComparer);
    }

    public void MoveBefore(LinkedListNode<T> before, LinkedListNode<T> node)
    {
        if (node == before)
            return;

        T value = node.Value;
        Remove(node);
        AddBefore(before, value);
    }

    public void MoveBefore(LinkedListNode<T> before, T value)
    {
        MoveBefore(before, Find(value));
    }

    public void MoveAfter(LinkedListNode<T> after, LinkedListNode<T> node)
    {
        if (after == node)
            return;

        T value = node.Value;
        Remove(node);
        AddAfter(after, value);
    }

    public void MoveAfter(LinkedListNode<T> after, T value)
    {
        MoveAfter(after, Find(value));
    }

    public void BringToFront(LinkedListNode<T> node)
    {
        MoveBefore(First, node);
    }

    public void BringToFront(T value)
    {
        MoveBefore(First, value);
    }

    public void BringToBack(LinkedListNode<T> node)
    {
        MoveAfter(Last, node);
    }

    public void BringToBack(T value)
    {
        MoveAfter(Last, value);
    }
}
