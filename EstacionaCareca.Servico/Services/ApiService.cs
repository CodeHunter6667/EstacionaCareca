using EstacionaCareca.Shared.Configuration;
using EstacionaCareca.Shared.DTOs;

namespace EstacionaCareca.Servico.Services;

public class ApiService
{
    public async Task<ResponseDto> GetPlate(RequestDto dto)
    {
        using (var httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(30) })
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {Configuration.ApiToken}");
            var formData = new MultipartFormDataContent();
            var imagem = File.ReadAllBytes(dto.CaminhoImagem);
            formData.Add(new ByteArrayContent(imagem), "upload", Path.GetFileName(dto.CaminhoImagem));
            formData.Add(new StringContent("br"), "regions");

            var response = await httpClient.PostAsync(Configuration.ApiBaseUrl, formData);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = System.Text.Json.JsonSerializer.Deserialize<ResponseDto>(jsonResponse);
                return result!;
            }
            else
            {
                throw new Exception($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
