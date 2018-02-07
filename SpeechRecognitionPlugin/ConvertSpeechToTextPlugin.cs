using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Odx.Xrm.Core;

namespace SpeechRecognitionPlugin
{
    public class ConvertSpeechToTextPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var organizationServiceFactory = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var orgService = organizationServiceFactory.CreateOrganizationService(null);

            tracingService.Trace("Plugin started");
            var target = context.InputParameters["Target"] as Entity;
            var postImage = context.PostEntityImages["PostImage"] as Entity;

            if(target.LogicalName == "annotation" && (target.GetAttributeValue<bool?>("isdocument") ?? false))
            {
                tracingService.Trace("Processing annotation");
                var mimetype = target.GetAttributeValue<string>("mimetype");
                tracingService.Trace("Mimetype: " + mimetype);
                if (mimetype == "audio/wav")
                {
                    tracingService.Trace("Getting document body");
                    var documentBody = target.GetAttributeValue<string>("documentbody");

                    tracingService.Trace("Convert body to bytes");
                    var byteContent = Convert.FromBase64String(documentBody);

                    tracingService.Trace("Create request");
                    HttpWebRequest request = null;
                    request = (HttpWebRequest)WebRequest.Create("https://speech.platform.bing.com/speech/recognition/interactive/cognitiveservices/v1?language=en-us&format=detailed");
                    request.SendChunked = true;
                    request.Accept = @"application/json;text/xml";
                    request.Method = "POST";
                    request.ProtocolVersion = HttpVersion.Version11;
                    request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
                    request.Headers["Ocp-Apim-Subscription-Key"] = "subscription_key_here";

                    using (Stream requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(byteContent, 0, byteContent.Length);
                        requestStream.Flush();
                        
                        using (var responseStream = new StreamReader(request.GetResponse().GetResponseStream()))
                        {
                            tracingService.Trace("Read response");
                            string response = responseStream.ReadToEnd();

                            var result = response.FromJSON<ServiceResponse>();

                            if(result.RecognitionStatus == "Success")
                            {
                                tracingService.Trace("Recognition successfull");

                                var bestConfidence = result.NBest.FirstOrDefault(n => n.Confidence == result.NBest.Max(r => r.Confidence));

                                var toUpdate = new Entity("annotation");
                                toUpdate.Id = target.Id;
                                toUpdate["notetext"] = bestConfidence?.Lexical;

                                orgService.Update(toUpdate);
                            }
                        }
                    }                    
                }
            }

            
        }
    }
}
