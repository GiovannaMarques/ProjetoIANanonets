using RestSharp;
using RestSharp.Authenticators;
using System;

namespace Projetovitao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var request = new RestRequest("/api/v2/OCR/Model/", Method.POST, DataFormat.Json);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", @"{""categories"" :[""CNPJ"", ""Documentação"", ""Endereço"", ""Razão_Social"", ""Temporalidade""], ""model_type"": ""ocr""}", ParameterType.RequestBody);
            //Cria o modelo
            IRestResponse response = CallNanonet(request);


            //Treinar o modelo
            request.AddHeader("accept", "Multipart/form-data");
            request.AddParameter("data", @"[
                {
                    ""filename"":""1.pdf"", ""object"": [
                    {""name"":""CNPJ"", ""ocr_text"":""text inside the bounding box"", ""bndbox"": { ""xmin"": 1,""ymin"": 1, ""xmax"": 100, ""ymax"": 100} },
                    {""name"":""Documentação"", ""ocr_text"":""text inside the bounding box"", ""bndbox"": { ""xmin"": 1,""ymin"": 1, ""xmax"": 100, ""ymax"": 100} },
                    {""name"":""Endereço"", ""ocr_text"":""text inside the bounding box"", ""bndbox"": { ""xmin"": 1,""ymin"": 1, ""xmax"": 100, ""ymax"": 100} },
                    {""name"":""Razão_Social"", ""ocr_text"":""text inside the bounding box"", ""bndbox"": { ""xmin"": 1,""ymin"": 1, ""xmax"": 100, ""ymax"": 100} },
                    {""name"":""Temporalidade"", ""ocr_text"":""text inside the bounding box"", ""bndbox"": { ""xmin"": 1,""ymin"": 1, ""xmax"": 100, ""ymax"": 100} }
                ]}
            ]");
            request.AddFile("file", "REPLACE_IMAGE_PATH.jpg");
            IRestResponse response = client.Execute(request); "

            var recuperaModelo = new RestRequest("/api/v2/OCR/Model/c576e8c2-feb6-4020-9d5d-b33bcc6ba53b", Method.GET, DataFormat.Json);
            IRestResponse recuperaModeloResposta = CallNanonet(request);
            // TODO: Criar Modelo -> Invoices 
        }

        private static IRestResponse CallNanonet(RestRequest request)
        {
            var client = new RestClient("https://app.nanonets.com");
            //client.Authenticator = new HttpBasicAuthenticator("0yNLNv6UN8ohApMFUfO27T-XnFUXI1gl", string.Empty);
            client.Authenticator = new HttpBasicAuthenticator("MBJ7eRbOi9XAIqoEhBNI5P5IElP8avni", string.Empty); 

            return client.Execute(request);
        }
    }
}
