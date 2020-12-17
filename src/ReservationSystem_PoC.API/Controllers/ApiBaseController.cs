using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem_PoC.API.Responses;
using ReservationSystem_PoC.Domain.Core.DomainHandlers;
using ReservationSystem_PoC.Domain.Core.DomainNotifications;
using ReservationSystem_PoC.Domain.Core.Interfaces;
using ReservationSystem_PoC.Domain.Core.Interfaces.Bus;
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
            return (!_notifications.HasNotificationsError());
        }


        protected ActionResult<T> ResponsePost<T>(string action, T result)
        {

            var route = GetActualRoute();


            if (!IsValidOperation())
                return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsErrorByKey()));


            if (result == null)
                return NoContent();


            var requestResponse = new RequestResponse<T>(result);


            //add all notifications of Error
            if (_notifications.HasNotificationsError())
            {
                var errors = _notifications.GetNotificationsError();
                foreach (var error in errors)
                {
                    requestResponse.AddMessageFailure(error.Value);

                }
            }

            //add all notifications of Success

            if (!_notifications.HasNotificationsSucess()) return CreatedAtAction(action, route, requestResponse);


            var msgs = _notifications.GetNotificationsSuccess();
            foreach (var msg in msgs)
            {
                requestResponse.AddMessageSucess(msg.Value);

            }


            return CreatedAtAction(action, route, requestResponse);

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
