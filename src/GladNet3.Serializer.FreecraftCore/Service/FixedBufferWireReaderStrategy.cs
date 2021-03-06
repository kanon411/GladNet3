﻿using FreecraftCore.Serializer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladNet
{
	//TODO: move this into FreecraftCore.Streams
	/// <summary>
	/// Implementation of the <see cref="IWireStreamReaderStrategy"/> that reads only as many bytes as specified from the
	/// an internally managed stream.
	/// </summary>
	public class FixedBufferWireReaderStrategy : DefaultStreamManipulationStrategy<Stream>, IWireStreamReaderStrategy
	{
		private int Count { get; }

		//TODO: Overloads that take the byte buffer instead
		public FixedBufferWireReaderStrategy([NotNull] byte[] bytes, int start, int count)
			: base(new MemoryStream(bytes), true)
		{
			if(bytes == null) throw new ArgumentNullException(nameof(bytes), $"Provided argument {nameof(bytes)} must not be null.");
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), $"Requested negative Count: {count}.");
			if(start < 0 || bytes.Length < start + count) throw new ArgumentOutOfRangeException(nameof(start));

			//Set the position of the stream to the start.
			ManagedStream.Position = start;
			Count = count;
		}

		public FixedBufferWireReaderStrategy([NotNull] Stream stream, int count)
			: base(stream, false)
		{
			if(stream == null) throw new ArgumentNullException(nameof(stream), $"Provided argument {nameof(stream)} must not be null.");
			if(count < 0) throw new ArgumentOutOfRangeException(nameof(count));

			Count = count;
		}

		public byte[] ReadAllBytes()
		{
			return ReadBytes(Math.Max(0, (int)(Count - ManagedStream.Position)));
		}

		public byte ReadByte()
		{
			if(Count == ManagedStream.Position)
				throw new InvalidOperationException("Failed to read a desired byte from the stream.");

			//would be -1 if it's invalid
			int b = ManagedStream.ReadByte();

			//TODO: Contract interface doesn't mention throwing in this case. Should we throw?
			if(b == -1)
				throw new InvalidOperationException("Failed to read a desired byte from the stream.");

			return (byte)b;
		}

		public byte[] ReadBytes(int count)
		{
			if(Count < ManagedStream.Position + count)
				throw new InvalidOperationException($"Failed to read a desired bytes from the stream. Count: {Count} Position: {ManagedStream.Position} Requested: {count}");

			byte[] bytes = new byte[count];

			int readCount = ManagedStream.Read(bytes, 0, count);

			if(readCount == 0 && count != 0)
				return null;

			return bytes;
		}

		public byte PeekByte()
		{
			if(Count == ManagedStream.Position)
				throw new InvalidOperationException("Failed to read a desired bytes from the stream.");

			byte b = ReadByte();

			//Move it back one
			ManagedStream.Position = ManagedStream.Position - 1;

			return b;
		}

		public byte[] PeekBytes(int count)
		{
			if(Count < ManagedStream.Position + count)
				throw new InvalidOperationException("Failed to read a desired bytes from the stream.");

			byte[] bytes = ReadBytes(count);

			if(bytes == null)
				return null;

			//Now move the stream back
			ManagedStream.Position = ManagedStream.Position - count;

			return bytes;
		}
	}
}
