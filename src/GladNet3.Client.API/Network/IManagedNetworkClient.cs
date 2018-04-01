﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GladNet
{
	/// <summary>
	/// Contract for a managed network client that provides
	/// a high level networking API to consumers.
	/// </summary>
	/// <typeparam name="TPayloadWriteType">The type of payload it should write.</typeparam>
	/// <typeparam name="TPayloadReadType">The type of payload it should read.</typeparam>
	public interface IManagedNetworkClient<TPayloadWriteType, TPayloadReadType> : IPeerPayloadSendService<TPayloadWriteType>, IConnectionService,
		INetworkMessageProducer<TPayloadReadType>, IPayloadInterceptable
		where TPayloadWriteType : class
		where TPayloadReadType : class
	{
		//TODO: Document
		void StartNetwork();

		void StopNetwork();
	}
}
