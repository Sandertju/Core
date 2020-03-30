﻿using System.Collections.Generic;
using System.Net;
using System.Web.Http.Cors;
using Resgrid.Model;
using Resgrid.Model.Services;
using Resgrid.Web.Services.Controllers.Version3.Models.Security;
using System.Net.Http;
using Resgrid.Web.Services.Controllers.Version3.Models.Commands;

namespace Resgrid.Web.Services.Controllers.Version3
{
	/// <summary>
	/// Operations to perform against the security sub-system
	/// </summary>
	public class CommandsController : V3AuthenticatedApiControllerbase
	{
		private readonly ICommandsService _commandsService;

		/// <summary>
		/// Operations to perform against the security sub-system
		/// </summary>
		public CommandsController(ICommandsService commandsService)
		{
			_commandsService = commandsService;
		}

		/// <summary>
		/// Gets all the command definitions for the department
		/// </summary>
		/// <returns>AllCommandsResult object with a list of CommandResult's for each command</returns>
		public AllCommandsResult GetAllCommands()
		{
			var result = new AllCommandsResult();
			result.Commmands = new List<CommandResult>();

			var commands = _commandsService.GetAllCommandsForDepartment(DepartmentId);

			if (commands != null && commands.Count > 0)
			{
				foreach (var command in commands)
				{
					var cmd = new CommandResult();
					cmd.Id = command.CommandDefinitionId;
					cmd.CallTypeId = command.CallTypeId;
					cmd.Timer = command.Timer;
					cmd.TimerMinutes = command.TimerMinutes;
					cmd.Name = command.Name;
					cmd.Description = command.Description;
					cmd.Assignments = new List<CommandAssignmentResult>();

					if (command.Assignments != null && command.Assignments.Count > 0)
					{
						foreach (var assignment in command.Assignments)
						{
							var ass = new CommandAssignmentResult();
							ass.RoleId = assignment.CommandDefinitionRoleId;
							ass.CommandId = assignment.CommandDefinitionId;
							ass.Name = assignment.Name;
							ass.Description = assignment.Description;
							ass.MinUnitPersonnel = assignment.MinUnitPersonnel;
							ass.MaxUnitPersonnel = assignment.MaxUnitPersonnel;
							ass.MaxUnits = assignment.MaxUnits;
							ass.MinTimeInRole = assignment.MinTimeInRole;
							ass.MaxTimeInRole = assignment.MaxTimeInRole;
							ass.ForceRequirements = assignment.ForceRequirements;
							ass.RequiredUnitTypes = new List<AssignmentUnitTypeResult>();
							ass.RequiredCerts = new List<AssignmentCertResult>();
							ass.RequiredRoles = new List<AssignmentPersonnelResult>();

							if (assignment.RequiredUnitTypes != null && assignment.RequiredUnitTypes.Count > 0)
							{
								foreach (var unitType in assignment.RequiredUnitTypes)
								{
									var ut = new AssignmentUnitTypeResult();
									ut.Id = unitType.CommandDefinitionRoleUnitTypeId;
									ut.RoleId = unitType.CommandDefinitionRoleId;
									ut.UnitTypeId = unitType.UnitTypeId;

									ass.RequiredUnitTypes.Add(ut);
								}
							}

							if (assignment.RequiredCerts != null && assignment.RequiredCerts.Count > 0)
							{
								foreach (var cert in assignment.RequiredCerts)
								{
									var rc = new AssignmentCertResult();
									rc.Id = cert.CommandDefinitionRoleCertId;
									rc.RoleId = cert.CommandDefinitionRoleId;
									rc.CertificationTypeId = cert.DepartmentCertificationTypeId;

									ass.RequiredCerts.Add(rc);
								}
							}

							if (assignment.RequiredRoles != null && assignment.RequiredRoles.Count > 0)
							{
								foreach (var role in assignment.RequiredRoles)
								{
									var rr = new AssignmentPersonnelResult();
									rr.Id = role.CommandDefinitionRolePersonnelRoleId;
									rr.RoleId = role.CommandDefinitionRoleId;
									rr.PersonnelRoleId = role.PersonnelRoleId;

									ass.RequiredRoles.Add(rr);
								}
							}

							cmd.Assignments.Add(ass);
						}
					}

					result.Commmands.Add(cmd);
				}
			}

			return result;
		}

		public HttpResponseMessage Options()
		{
			var response = new HttpResponseMessage();
			response.StatusCode = HttpStatusCode.OK;
			response.Headers.Add("Access-Control-Allow-Origin", "*");
			response.Headers.Add("Access-Control-Request-Headers", "*");
			response.Headers.Add("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS");

			return response;
		}
	}
}
