using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class LinkedList<T> : IEnumerable<T>
    {
        // A type to hold a value and 
        // point to another value.
        public delegate bool predicate(T tested);
        public delegate K assignGroup<K>(T toAssign);
        public delegate K applyMap<K>(T toMap);
        class ListElement<AlsoT>
        {
            public AlsoT value { get; set; }
            public ListElement<AlsoT> next { get; set; }
            public ListElement(AlsoT value)
            {
                this.value = value;
            }

            public ListElement(AlsoT value, ListElement<AlsoT> next)
            {
                this.next = next;
                this.value = value;
            }

        }

        // Holds the first element,
        // the last element,
        // and the total number of elements.
        class ElementProvider
        {
            public ListElement<T> firstElement;
            public ListElement<T> lastElement;
            public int numberOfElements;

            public ElementProvider()
            {
                numberOfElements = 0;
                lastElement = new ListElement<T>(default(T));
                firstElement = new ListElement<T>(default(T), lastElement);
            }
        }

        ElementProvider elements;

        // The element currently being
        // considered by the linked list.
        ListElement<T> considering;
        ListElement<T> behindConsidering;
        int indexOn;

        public LinkedList()
        {
            elements = new ElementProvider();
            considering = elements.firstElement;
            indexOn = -1;
        }
        // Creates a shallow copy of toCopy.
        public LinkedList(LinkedList<T> toCopy)
        {
            this.elements = toCopy.elements;
            considering = this.elements.firstElement;
            indexOn = -1;
        }

        public LinkedList(IEnumerable<T> toCopy)
        {
            elements = new ElementProvider();
            ListElement<T> building = elements.firstElement;
            foreach (T element in toCopy)
            {
                building.next = new ListElement<T>(element);
                building = building.next;
            }
            building.next = elements.lastElement;
        }

        // Returns the current index pointed at
        // by this linked list
        public int CurrentIndex()
        {
            return indexOn;
        }

        // Adds a value to the beginning of
        // the linked list.
        public void AddAtStart(T value)
        {
            elements.numberOfElements++;
            elements.firstElement.next = new ListElement<T>(value, elements.firstElement.next);
            if (indexOn >= 0)
            {
                indexOn++;
            }
        }

        // Adds a value to the linked list after
        // the element being considered.
        public void AddAfter(T value)
        {
            elements.numberOfElements++;
            considering.next = new ListElement<T>(value, considering.next);
        }

        // Advances the considered element up by one.
        public void MoveUp()
        {
            if (considering.next != elements.lastElement)
            {
                behindConsidering = considering;
                considering = considering.next;
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index out of range!");
            }
            indexOn++;
        }

        // Sets the value being considered
        // to just before index 0;
        public void ResetPointer()
        {
            considering = elements.firstElement;
            behindConsidering = elements.firstElement;
            indexOn = -1;
        }

        // Indicates if invoking moveUp is safe.
        public bool ElementsRemain()
        {
            return considering.next != elements.lastElement &&
                considering != elements.lastElement;
        }

        // Returns the number of elements
        // in the linked list.
        public int Length()
        {
            return elements.numberOfElements;
        }

        // Removes the element being considered.
        public void Remove()
        {
            if (considering != elements.lastElement && considering != elements.firstElement)
            {
                elements.numberOfElements--;
                behindConsidering.next = considering.next;
                considering = behindConsidering.next;
            }
        }

        // Returns the value of the element
        // being considered.
        public T Value()
        {
            return considering.value;
        }

        // Accesses a given element.
        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", "Negative index requested!");
                }
                if (indexOn > index)
                {
                    this.ResetPointer();
                }

                for (int i = indexOn; i < index; i++)
                {
                    MoveUp();
                }
                return considering.value;
            }
            set
            {
                if (index < 0)
                {
                    throw new ArgumentOutOfRangeException("index", "Negative index requested!");
                }
                if (indexOn > index)
                {
                    this.ResetPointer();
                }

                for (int i = indexOn; i < index; i++)
                {
                    if (!ElementsRemain())
                    {
                        AddAfter(default(T));
                    }
                    MoveUp();
                }
                considering.value = value;
            }

        }

        // Creates a linked list consisting of
        // the combination of elements from
        // two other linked lists.
        public static LinkedList<T> operator +(LinkedList<T> firstList, LinkedList<T> secondList)
        {
            LinkedList<T> returnList = new LinkedList<T>();
            foreach (T listValue in firstList)
            {
                returnList.AddAfter(listValue);
                returnList.MoveUp();
            }
            foreach (T listValue in secondList)
            {
                returnList.AddAfter(listValue);
                returnList.MoveUp();
            }
            returnList.ResetPointer();
            return returnList;
        }

        public LinkedList<T> Slice(int start, int end)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", "Negative index requested!");
            }
            if (end > elements.numberOfElements)
            {
                throw new ArgumentOutOfRangeException("end", "Index too high!");
            }

            LinkedList<T> returnList = new LinkedList<T>();
            if (start < indexOn)
            {
                ResetPointer();
            }
            else
            {
                start -= indexOn;
                end -= indexOn;
            }

            for (int i = 0; i <= start; i++)
            {
                MoveUp();
            }

            for (int i = start; i < end; i++)
            {
                returnList.AddAfter(Value());
                returnList.MoveUp();
                this.MoveUp();
            }

            return returnList;
        }

        public System.Collections.Generic.LinkedList<T> toSystemLinkedList()
        {
            var returnList = new System.Collections.Generic.LinkedList<T>();
            foreach (T element in this)
            {
                returnList.AddLast(element);
            }
            return returnList;
        }

        public static implicit operator System.Collections.Generic.LinkedList<T>(LinkedList<T> toConvert) {
            return toConvert.toSystemLinkedList();
        }

        public static implicit operator LinkedList<T>(System.Collections.Generic.LinkedList<T> toConvert)
        {
            var returnList = new LinkedList<T>();
            returnList.ResetPointer();
            foreach (T element in toConvert)
            {
                returnList.AddAfter(element);
                returnList.MoveUp();
            }
            return returnList;
        }

        // Sends an enumerator, allowing
        // a foreach loop to be used.
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)new LinkedListEnum<T>(this);
        }

        public LinkedList<T> Filter(predicate tester)
        {
            var returnList = new LinkedList<T>();
            ListElement<T> testedElement = elements.firstElement.next;
            while (testedElement != elements.lastElement)
            {
                if (tester(testedElement.value))
                {
                    returnList.AddAfter(testedElement.value);
                    returnList.MoveUp();
                }
                testedElement = testedElement.next;
            }
            return returnList;
        }


        public BinarySearchTree<K, LinkedList<T>> Group<K>(
            BinarySearchTree<K, LinkedList<T>>.compareDelegate SortFunction,
            assignGroup<K> groupFunction)
        {
            var returnTree = new BinarySearchTree<K, LinkedList<T>>(
                SortFunction
                );
            LinkedList<T> looper = new LinkedList<T>(this);
            foreach (T element in looper)
            {
                K groupFound = groupFunction(element);
                if (!returnTree.has(groupFound))
                {
                    returnTree.add(groupFound, new LinkedList<T>());
                }
                returnTree.search(groupFound).AddAtStart(element);
            }
            return returnTree;
        }
    }

    // Provides functionality for
    // a foreach loop.
    public class LinkedListEnum<T> : IEnumerator<T>
    {
        LinkedList<T> listToEnumerate;

        public LinkedListEnum(LinkedList<T> listToEnumerate)
        {
            this.listToEnumerate = listToEnumerate;
            this.listToEnumerate.ResetPointer();
            /*if(this.listToEnumerate.elementsRemain()){
                this.listToEnumerate.moveUp();
            }*/
        }

        public bool MoveNext()
        {
            if (listToEnumerate.ElementsRemain())
            {
                listToEnumerate.MoveUp();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            listToEnumerate.ResetPointer();
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return listToEnumerate.Value();
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        public void Dispose()
        {

        }


    }
}