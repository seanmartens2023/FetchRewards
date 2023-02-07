/*
 * OpenAIDateTimeCalculator
 *
 * An IPointsCalculator implementation that builds a query to be 
 * answered through OpenAI's davinci-003 GPT API.
 * This class makes an async call to deliver a JSON payload to OpenAI's servers, then parses and returns the proper response.
 * 
 * I thought this would be a fun addition while thinking about the ability to handle fuzzy inputs.
 * More importantly it was added as an excersise in async operations reaching out to external APIs.
 * 
 * Expected Error: Needs Bearer token to run, also the answers might be bad until they roll out an API for v3 or v3.5.
 */
using FetchDevSample.Models;
using Newtonsoft.Json;
using System.Text;

namespace FetchDevSample.Calculators
{
    public class OpenAIDateTimeCalculator : IPointsCalculator
    {
        public long CalculatePoints(Receipt receipt)
        {
            int ruleNumber = 1;

            StringBuilder modelBuilder = new StringBuilder();
            modelBuilder.Append(
$@"Calculate the total score by adding these rules. Return only the number for the total score.
Rule {ruleNumber++}) start with the score equal to 0
Rule {ruleNumber++}) if the integer {receipt.PurchaseDate.Day} is an odd number, add 6 points
Rule {ruleNumber++}) if the time: '{receipt.PurchaseTime}' is between 14:00 and 16:00, add 10 points");

            Task<ChatGptResponse> inflightQuestion = AskQuestion(
                modelBuilder.ToString()
             );

            var openAiResponse = inflightQuestion.GetAwaiter().GetResult();
            string answer = openAiResponse?.Choices?.First().Answer ?? "0";
            answer = string.Concat(answer.Where(c => Char.IsNumber(c)));
            return long.Parse(answer);
        }

        static async Task<ChatGptResponse> AskQuestion(string question)
        {
            var url = "https://api.openai.com/v1/completions";
            var client = new HttpClient();

            try
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "<BEARER_TOKEN_HERE>");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var body = new Dictionary<string, object>
                {
                    ["model"] = "text-davinci-003",
                    ["prompt"] = question,
                    ["max_tokens"] = (int)64
                };
                var content = new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json");

                var post = await client.PostAsync(url, content);
                var responseString = await post.Content.ReadAsStringAsync();
                var gptResponse = JsonConvert.DeserializeObject<ChatGptResponse>(responseString);

                if (gptResponse is null)
                {
                    throw new NullReferenceException("AI reponse missing");
                }

                return gptResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return new ChatGptResponse
                {
                    Choices = new List<ChatGptResponse.ChatGptChoices> {
                        new ChatGptResponse.ChatGptChoices{Answer = e.Message}
                    }
                };
            }
        }

        public class ChatGptResponse
        {
            [JsonProperty("choices")]
            public List<ChatGptChoices>? Choices { get; set; }
            [JsonProperty("usage")]
            public ApiUsage? Usage { get; set; }
            public class ChatGptChoices
            {
                [JsonProperty("text")]
                public string? Answer { get; set; }
            }
            public class ApiUsage
            {
                [JsonProperty("total_tokens")]
                int TokensUsed { get; set; }
            }
        }
    }
}
