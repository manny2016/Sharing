using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Sharing.Core
{
    public class DefaultRandomGenerator: IRandomGenerator
    {

        //private static RandomGenerator instance;
        //private static object lockObj = new object();
        //public static RandomGenerator Instance
        //{
        //    get
        //    {
        //        lock (lockObj)
        //        {
        //            if (instance == null)
        //            {
        //                lock (lockObj)
        //                {
        //                    if (instance == null)
        //                        instance = new RandomGenerator();
        //                }
        //            }
        //        }
        //        return instance;
        //    }
        //}

        readonly RNGCryptoServiceProvider csp;

        public DefaultRandomGenerator()
        {
            csp = new RNGCryptoServiceProvider();
        }

        private int Next(int minValue, int maxExclusiveValue)
        {
            if (minValue >= maxExclusiveValue)
                throw new ArgumentOutOfRangeException("minValue must be lower than maxExclusiveValue");

            long diff = (long)maxExclusiveValue - minValue;
            long upperBound = uint.MaxValue / diff * diff;

            uint ui;
            do
            {
                ui = GetRandomUInt();
            } while (ui >= upperBound);
            return (int)(minValue + (ui % diff));
        }

        public uint GetRandomUInt()
        {
            var randomBytes = GenerateRandomBytes(sizeof(uint));
            return BitConverter.ToUInt32(randomBytes, 0);
        }
        public string Genernate()
        {
            return GetRandomUInt().ToString();
        }
        private byte[] GenerateRandomBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            csp.GetBytes(buffer);
            return buffer;
        }
    }
}
