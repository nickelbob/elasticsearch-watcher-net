﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Elasticsearch.Net;

namespace Nest.Watcher.Tests.Integration.Acknowledge
{
	[TestFixture]
	public class Basic : IntegrationTest
	{
		[Test]
		public override void Fluent()
		{
			var watchId = PutWatch();
			var health = this.Client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow));
			var ackResponse = this.Client.AcknowledgeWatch(watchId);
			Assert(ackResponse);
		}

		[Test]
		public override void ObjectInitializer()
		{
			var watchId = PutWatch();
			var health = this.Client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow));
			var ackResponse = this.Client.AcknowledgeWatch(new AcknowledgeWatchRequest(watchId));
			Assert(ackResponse);
		}

		protected virtual void Assert(IAcknowledgeWatchResponse response)
		{
			response.IsValid.Should().BeTrue();
			response.ConnectionStatus.HttpStatusCode.Should().Be(200);
			response.Status.Should().NotBeNull();
			response.Status.Actions.Should().NotBeEmpty();
			var testIndex = response.Status.Actions["test_index"];
			testIndex.Acknowledgement.Should().NotBeNull();
			testIndex.Acknowledgement.Timestamp.Should().NotBeNull();
			testIndex.Acknowledgement.State.Should().Be("awaits_successful_execution");
		}
	}
}
