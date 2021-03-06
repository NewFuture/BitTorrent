﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Torrent.Client
{
    public class InfoHash : IEnumerable<byte>
    {
        private readonly byte[] innerArray;

        public InfoHash(byte[] bytes)
        {
            Contract.Requires(bytes.Length == 20);

            innerArray = new byte[bytes.Length];
            Buffer.BlockCopy(bytes, 0, innerArray, 0, bytes.Length);
        }

        public int Length
        {
            get { return 20; }
        }

        public byte this[int index]
        {
            get { return innerArray[index]; }
        }

        #region IEnumerable<byte> Members

        public IEnumerator<byte> GetEnumerator()
        {
            return innerArray.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerArray.GetEnumerator();
        }

        #endregion

        public static implicit operator InfoHash(byte[] bytes)
        {
            return new InfoHash(bytes);
        }

        public static implicit operator byte[](InfoHash infoHash)
        {
            var newArray = new byte[infoHash.Length];
            Buffer.BlockCopy(infoHash.innerArray, 0, newArray, 0, infoHash.Length);
            return newArray;
        }

        public static bool operator !=(InfoHash a, InfoHash b)
        {
            return !(a == b);
        }

        public static bool operator ==(InfoHash a, InfoHash b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object) a == null) return false;
            return a.Equals(b);
        }

        public override string ToString()
        {
            return BitConverter.ToString(innerArray).Replace("-", "");
        }

        public override bool Equals(object obj)
        {
            var infoHash = obj as InfoHash;
            return obj != null && this.SequenceEqual(infoHash);
        }

        public override int GetHashCode()
        {
            return innerArray[0] + innerArray[4] + innerArray[9] + innerArray[14] + innerArray[19];
        }
    }
}