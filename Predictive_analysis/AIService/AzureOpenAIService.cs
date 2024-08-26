using Azure.AI.OpenAI;
using Azure;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Predictive_analysis
{
    internal class AzureOpenAIService 
    {
        const string endpoint = "https://yourEndpoint.openai.azure.com";
        const string deploymentName = "GPT35Turbo";
        string key = "";

        public AzureOpenAIService()
        {

        }

        /// <summary>
        /// Initialize local embeddings for the provided text chunks
        /// </summary>
        /// <param name="chunks"></param>
        /// <returns></returns>
        public Task Initialize(string[] chunks)
        {
            return Task.CompletedTask;
        }

        public Task<ObservableCollection<ForecastModel>> GetAnswerFromGPT(string userPrompt, ForecastViewModel viewModel, int index)
        {
            var chatCompletionsOptions = new ChatCompletionsOptions
            {
                DeploymentName = deploymentName,
                Temperature = (float)0.5,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
            };

            // Add the system message and user message to the options
            chatCompletionsOptions.Messages.Add(new ChatRequestUserMessage(userPrompt));
            try
            {
                throw new NotImplementedException("");
              
                    //var client = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
                    //var response = client.GetChatCompletionsAsync(chatCompletionsOptions);
                    //var content = response.Value.Choices[0].Message.Content;
            }
            catch (Exception)
            {
                return Task.FromResult(viewModel.GenerateDataSource(index));
            }
        }

        public ObservableCollection<ForecastModel> ConvertToCompaniesModelCollection(string data)
        {
            if(string.IsNullOrEmpty(data))
            {
                return new ObservableCollection<ForecastModel>();
            }

            var stockData = new ObservableCollection<ForecastModel>();

            var lines = data.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('\t');
                if (parts.Length == 5)
                {
                    var date = DateTime.ParseExact(parts[0].Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    var high = double.Parse(parts[1].Trim());
                    var low = double.Parse(parts[2].Trim());
                    var open = double.Parse(parts[3].Trim());
                    var close = double.Parse(parts[4].Trim());

                    stockData.Add(new ForecastModel(date, high, low, open, close));
                }
            }

            return stockData;
        }

        internal string GeneratePrompt(List<ForecastModel> historicalData)
        {
            var prompt = "Predicted OHLC values for the next 5 time step(s) for the following data:\n";
            foreach (var data in historicalData)
            {
                prompt += $"{data.Date:yyyy-MM-dd}: {data.High}, {data.Low}, {data.Open}, {data.Close}\n";
            }
            prompt += "and the prediction output data should be in the yyyy-MM-dd:High:Low:Open:Close, no other explaination required\n";
            return prompt;
        }
    }
}
