// <copyright file="Ringbuffer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SerialPortReaderWriter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Ringbuffer used for read actions on a communication interface
    /// </summary>
    /// <typeparam name="T">Generic type parameter for ring buffer.</typeparam>
    public class Ringbuffer<T> : IEnumerable<T>, IEnumerable, ICollection
    {
        /// <summary>
        /// Default size of the ring buffer.
        /// </summary>
        private const int DefaultBufferSize = 256;

        /// <summary>
        /// Default size of the ring buffer.
        /// </summary>
        private int bufferSize;

        /// <summary>
        /// Least added element in the buffer.
        /// </summary>
        private int head = 0;

        /// <summary>
        /// Recently added element in the buffer.
        /// </summary>
        private int tail = 0;

        /// <summary>
        /// Buffer which is used to store data of type <see cref="T"/>.
        /// </summary>
        private T[] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ringbuffer{T}"/> class.
        /// </summary>
        public Ringbuffer()
            : this(DefaultBufferSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ringbuffer{T}"/> class.
        /// </summary>
        /// <param name="bufferSize">Size of the ring buffer.</param>
        public Ringbuffer(int bufferSize)
        {
            this.bufferSize = bufferSize;
            this.buffer = new T[bufferSize];
        }

        /// <inheritdoc/>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Gets the number of elements currently contained in the buffer.
        /// </summary>
        public int Size { get { return this.Count; } }

        /// <inheritdoc/>
        bool ICollection.IsSynchronized => false;

        /// <inheritdoc/>
        object ICollection.SyncRoot => this;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the RingBuffer.
        /// </summary>
        private object SyncRoot => this;

        /// <summary>
        /// Retrieve the next item from the buffer.
        /// </summary>
        /// <returns>The oldest item added to the buffer.</returns>
        /// <exception cref="InvalidOperationException">Is thrown if the buffer is empty.</exception>
        public T Front()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Buffer is empty");
            }

            T tmpItem = this.buffer[this.head];
            this.head = (this.head + 1) % this.bufferSize;
            this.Count--;
            return tmpItem;
        }

        /// <summary>
        /// Retrieve the next item from the buffer.
        /// </summary>
        /// <returns>The oldest item added to the buffer.</returns>
        /// <exception cref="InvalidOperationException">Is thrown if the buffer is empty.</exception>
        public T Back()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("Buffer is empty");
            }

            T tmpItem = this.buffer[this.tail];
            this.head = (this.head + 1) % this.bufferSize;
            this.Count--;

            return tmpItem;
        }

        /// <summary>
        /// Add item to the ring buffer
        /// </summary>
        /// <param name="item">Item of type <see cref="T"/> to add to the ring buffer.</param>
        public void Add(T item)
        {
            this.Put(item);
        }

        /// <summary>
        /// Adds an item to the end of the buffer.
        /// </summary>
        /// <param name="item">The item to be added.</param>
        /// <exception cref="InvalidOperationException">Is thrown if the buffer is full.</exception>
        public void Put(T item)
        {
            // Buffer is full
            if (this.tail == this.head && this.Count != 0)
            {
                throw new InvalidOperationException("Buffer is full");
            }

            // Buffer is empty and implicit Tail == Head
            else if (this.Count == 0)
            {
                this.AddToBuffer(item);
            }

            // Buffer has space for a new item and implicit Tail != Head.
            else
            {
                this.AddToBuffer(item);
            }
        }

        /// <summary>
        /// Get <see cref="IEnumerator{T}"/> for the ring buffer.
        /// </summary>
        /// <returns><see cref="IEnumerator{T}"/> on the head of the ringbuffer</returns>
        public IEnumerator<T> GetEnumerator()
        {
            int currentIdx = this.head;
            for (int i = 0; i < this.Count; i++, currentIdx = (currentIdx + 1) % this.bufferSize)
            {
                yield return this.buffer[currentIdx];
            }
        }

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Determines whether the RingBuffer contains a specific value.
        /// </summary>
        /// <param name="item">The value to check the RingBuffer for.</param>
        /// <returns>True if the RingBuffer contains <paramref name="item"/>
        /// , false if it does not.
        /// </returns>
        public bool Contains(T item)
        {
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            int currentIdx = this.head;
            for (int i = 0; i < this.Count; i++, currentIdx = (currentIdx + 1) % this.bufferSize)
            {
                if (comparer.Equals(item, this.buffer[currentIdx]))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Clear the ring buffer from all elements.
        /// </summary>
        public void Clear()
        {
            for (int idx = 0; idx < this.bufferSize; idx++)
            {
                this.buffer[idx] = default;
            }

            this.head = 0;
            this.tail = 0;
            this.Count = 0;
        }

        /// <summary>
        /// Copies the contents of the RingBuffer to <paramref name="array"/>
        /// starting at <paramref name="arrayIndex"/>.
        /// </summary>
        /// <param name="array">The array to be copied to.</param>
        /// <param name="arrayIndex">The index of <paramref name="array"/>
        /// where the buffer should begin copying to.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            int currentIdx = this.head;
            for (int i = 0; i < this.Count; i++, arrayIndex++, currentIdx = (currentIdx + 1) % this.bufferSize)
            {
                array[arrayIndex] = this.buffer[currentIdx];
            }
        }

        /// <summary>
        /// Copies the elements of the RingBuffer to <paramref name="array"/>, 
        /// starting at a particular Array <paramref name="index"/>.
        /// </summary>
        /// <param name="array">The one-dimensional Array that is the 
        /// destination of the elements copied from RingBuffer. The Array must 
        /// have zero-based indexing.</param>
        /// <param name="index">The zero-based index in 
        /// <paramref name="array"/> at which copying begins.</param>
        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo((T[])array, index);
        }

        /// <summary>
        /// Add a new item to the buffer.
        /// </summary>
        /// <param name="toAdd">Item to add.</param>
        protected void AddToBuffer(T toAdd)
        {
            this.Count++;
            this.buffer[this.tail] = toAdd;
            this.tail = (this.tail + 1) % this.bufferSize;
        }
    }
}
