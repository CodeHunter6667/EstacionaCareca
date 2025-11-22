using EstacionaCareca.Servico.Services;
using EstacionaCareca.Shared.DTOs;

var servico = new ApiService();

var requestDto = new RequestDto();
requestDto.CaminhoImagem = "C:\\Dev\\Projetos CSharp\\ProjetoEstacionamento\\EstacionaCareca\\EstacionaCareca.Servico\\Imagens\\placa-mercosul-o-que-e.jpg";


var response = await servico.GetPlate(requestDto);
