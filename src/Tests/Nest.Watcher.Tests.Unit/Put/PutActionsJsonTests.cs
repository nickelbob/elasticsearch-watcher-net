﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Elasticsearch.Net;
using NUnit.Framework;

namespace Nest.Watcher.Tests.Unit.Put
{
	[TestFixture]
	public class PathActionJsonTests : UnitTest
	{
		[Test]
		public void EmailAction()
		{
			var expectedRequest = new
			{
				actions = new
				{
					emailSomeone = new
					{
						email = new
						{
							account = "account",
							from = "from",
							to = new [] {"to"},
							cc = new [] { "cc" },
							bcc = new [] { "dotnet@test.example" },
							reply_to = new [] { "replyto" },
							subject = "subject",
							body = new { html = "x" },
							priority = "high",
							attach_data = new { x = 1, y = "x" },
							transform = new
							{
								script = new { inline = "inline" }
							}
						}
					}
				}
			};
			var response = this.Client.PutWatch("some-watch", p => p
				.Actions(a => a
					.Add("emailSomeone", new EmailAction
					{
						Account = "account",
						AttachData = new { x = 1, y = "x" },
						Bcc = new [] {"dotnet@test.example"},
						Body = new EmailBody { Html = "x" },
						Cc = new [] { "cc" },
						From = "from",
						Priority = EmailPriority.High,
						ReplyTo = new [] { "replyto" },
						Subject = "subject",
						To = new [] {"to"},
						Transform = new ScriptTransform
						{
							Inline = "inline"
						}
					})
				)
			);
			this.JsonEquals(expectedRequest, response);
		}

		[Test]
		public void LogAction()
		{
			var expectedRequest = new
			{
				actions = new
				{
					logSomething = new
					{
						logging = new
						{
							text = "text",
							category = "category",
							level = "debug",
							transform = new
							{
								script = new { inline = "inline" }
							}
						}
					}
				}
			};
			var response = this.Client.PutWatch("some-watch", p => p
				.Actions(a => a
					.Add("logSomething", new LoggingAction
					{
						Text = "text",
						Category = "category",
						Level =  LogLevel.Debug,
						Transform = new ScriptTransform
						{
							Inline = "inline"
						}
					})
				)
			);
			this.JsonEquals(expectedRequest, response);
		}

		[Test]
		public void WebHook()
		{
			var expectedRequest = new
			{
				actions = new
				{
					webhook = new
					{
						webhook = new
						{
							scheme = "https",
							host = "host",
							port = 80,
							method = "post",
							path = "/some/path",
							body = "{ \"foo\": \"bar\" }",
							transform = new
							{
								script = new { inline = "inline" }
							}
						}
					}
				}
			};
			var response = this.Client.PutWatch("some-watch", p => p
				.Actions(a => a
					.Add("webhook", new WebhookAction
					{
						Scheme = ConnectionScheme.Https,
						Host = "host",
						Port = 80,
						Method = HttpMethod.Post,
						Path = "/some/path",
						Body = "{ \"foo\": \"bar\" }",
						Transform = new ScriptTransform
						{
							Inline = "inline"
						}
					})
				)
			);
			this.JsonEquals(expectedRequest, response);
		}

        [Test]
        public void SlackAction()
        {
            var expectedRequest = new
            {
                actions = new
                {
                    slack = new
                    {
                        slack = new
                        {
                            account = "slackAccount",
                            message = new 
                            {
                                to = new[] { "#general" },
                                text = "Testing Slack Watcher Integration"
                            }
                        }
                    }
                }
            };
            var response = this.Client.PutWatch("some-watch", p => p
                .Actions(a => a
                    .Add("slack", new SlackAction
                    {
                        Account = "slackAccount",
                        Message = new Message
                        {
                            Text = "Testing Slack Watcher Integration",
                            To = new[] { "#general" }
                        }
                    })
                )
            );
            this.JsonEquals(expectedRequest, response);
        }


    }
}
