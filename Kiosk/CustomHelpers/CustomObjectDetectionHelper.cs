using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace ServiceHelpers
{
    public static class CustomObjectDetectionHelper
    {

        private static PredictionEndpoint visionClient { get; set; }

        static CustomObjectDetectionHelper()
        {
            InitializeCustomObjectDetection();
        }

        private static string predictionApiKey;
        public static string PredictionApiKey
        {
            get
            {
                return predictionApiKey;
            }

            set
            {
                var changed = predictionApiKey != value;
                predictionApiKey = value;
                if (changed)
                {
                    InitializeCustomObjectDetection();
                }
            }
        }

        private static string projectId;
        public static string ProjectId
        {
            get
            {
                return projectId;
            }

            set
            {
                var changed = projectId != value;
                projectId = value;
                if (changed)
                {
                    InitializeCustomObjectDetection();
                }
            }
        }



        private static void InitializeCustomObjectDetection()
        {
            // Create a prediction endpoint, passing in the obtained prediction key
            visionClient = new PredictionEndpoint() { ApiKey = PredictionApiKey };
        }

        public static async Task<ImagePrediction> AnalyzeImageAsync(string imageUrl)
        {
           ImageUrl imgUrl = new ImageUrl(imageUrl);
            using (var stream = File.OpenRead(imageUrl))
            {
                return await visionClient.PredictImageAsync(new Guid(projectId), stream);
            }    
        }
    }
}
