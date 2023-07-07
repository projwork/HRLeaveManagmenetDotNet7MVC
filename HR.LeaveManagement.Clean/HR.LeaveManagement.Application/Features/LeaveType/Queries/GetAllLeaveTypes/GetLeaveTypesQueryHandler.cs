using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Logging;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        this._leaveTypeRepository = leaveTypeRepository;
        this._logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var leaveTypes = await _leaveTypeRepository.GetAsync();

        // convert data objects to DTO objects
        var data = new List<LeaveTypeDto>();

        foreach (var leaveType in leaveTypes)
        {
            var leaveTypeDto = new LeaveTypeDto()
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                DefaultDays = leaveType.DefaultDays
            };
            data.Add(leaveTypeDto);
        }

        // return list of DTO object
        _logger.LogInformation("Leave types were retrieved successfully");
        return data;
    }
}