using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem_PoC.Domain.Core.DomainHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem_PoC.API.Controllers
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        private readonly DomainNotificationHandler _notifications;
        protected readonly IMediatorHandler Mediator;
        protected ApiBaseController(IDependencyResolver dependencyResolver)
        {
            _notifications = (DomainNotificationHandler)dependencyResolver.Resolve<INotificationHandler<DomainNotification>>();

            Mediator = dependencyResolver.Resolve<IMediatorHandler>();

        }


        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }


        protected ActionResult ResponsePutPatch()
        {
            if (IsValidOperation())
            {
                return NoContent();
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult<T> ResponseDelete<T>(T item)
        {
            if (IsValidOperation())
            {
                if (item == null)
                    return NoContent();

                return Ok(item);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult<T> ResponsePost<T>(string action, T result)
        {

            var route = GetActualRoute();


            if (!IsValidOperation())
                return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));


            if (result == null)
                return NoContent();

            return CreatedAtAction(action, route, result);

        }
        protected ActionResult<IEnumerable<T>> ResponseGet<T>(IEnumerable<T> result)
        {

            if (result == null || !result.Any())
                return NoContent();

            return Ok(result);
        }

        protected ActionResult<T> ResponseGet<T>(T result)
        {
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(erroMsg);
            }
        }


        protected ActionResult ModelStateErrorResponseError()
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        protected void NotifyError(string message)
        {
            Mediator.NotifyDomainNotification(DomainNotification.Fail(message));
        }

        protected string GetActualRoute()
        {
            var endpoint = HttpContext.Request.Path.Value;

            return endpoint;
        }

    }
}
