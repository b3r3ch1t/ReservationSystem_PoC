﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMediatorHandler _mediator;

        protected ApiBaseController(IDependencyResolver dependencyResolver)
        {
            _notifications = (DomainNotificationHandler)dependencyResolver.Resolve<INotificationHandler<DomainNotification>>();

            _mediator = dependencyResolver.Resolve<IMediatorHandler>();

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

        protected ActionResult<T> ResponsePost<T>(string action, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, route, result);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
        }

        protected ActionResult<T> ResponsePost<T>(string action, string controller, object route, T result)
        {
            if (IsValidOperation())
            {
                if (result == null)
                    return NoContent();

                return CreatedAtAction(action, controller, route, result);
            }

            return BadRequest(new ValidationProblemDetails(_notifications.GetNotificationsByKey()));
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
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected ActionResult ModelStateErrorResponseError()
        {
            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.NotifyDomainNotification(new DomainNotification(code, message));
        }
    }
}
