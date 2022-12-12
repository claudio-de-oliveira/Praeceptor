using ErrorOr;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pea.Api.Common.Http;

namespace Pea.Api.Controllers;

[ApiController]
[Authorize]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        var statusCode = firstError.Type switch
        {
            /// Essa resposta significa que o servidor não entendeu a requisição pois está com uma sintaxe inválida.
            ErrorType.Validation => StatusCodes.Status400BadRequest,
//             /// Embora o padrão HTTP especifique "unauthorized", semanticamente, essa resposta significa "unauthenticated". Ou seja, o cliente deve se autenticar para obter a resposta solicitada.
//             ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
//             /// O cliente não tem direitos de acesso ao conteúdo portanto o servidor está rejeitando dar a resposta. Diferente do código 401, aqui a identidade do cliente é conhecida.
//             ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            /// O servidor não pode encontrar o recurso solicitado. Este código de resposta talvez seja o mais famoso devido à frequência com que acontece na web.
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            /// O método de solicitação é conhecido pelo servidor, mas foi desativado e não pode ser usado.
//             ErrorType.MethodNotAllowed => StatusCodes.Status405MethodNotAllowed,
//             /// Esta resposta será enviada quando uma requisição conflitar com o estado atual do servidor.
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}

