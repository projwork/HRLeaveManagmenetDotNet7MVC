using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetail;

public class GetLeaveRequestDetailQueryHandler : IRequestHandler<GetLeaveRequestDetailQuery, LeaveRequestDetailsDto>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    //private readonly IUserService _userService;

    public GetLeaveRequestDetailQueryHandler(ILeaveRequestRepository leaveRequestRepository
        /*IUserService userService*/)
    {
        _leaveRequestRepository = leaveRequestRepository;
        //this._userService = userService;
    }
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailQuery request, CancellationToken cancellationToken)
    {
        var lRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);
        var leaveRequest = new LeaveRequestDetailsDto()
        {
            Id = lRequest.Id,
            LeaveTypeId = lRequest.LeaveTypeId,
            StartDate = lRequest.StartDate,
            EndDate = lRequest.EndDate,
            RequestComments = lRequest.RequestComments,
            RequestingEmployeeId = lRequest.RequestingEmployeeId,
            Approved = lRequest.Approved,
            Cancelled = lRequest.Cancelled,
            DateRequested = DateTime.Now,
            LeaveType = new LeaveTypeDto()
            {
                Id = lRequest.LeaveType.Id,
                Name = lRequest.LeaveType.Name,
                DefaultDays = lRequest.LeaveType.DefaultDays
            }
        };

        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        // Add Employee details as needed
        //leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.RequestingEmployeeId);

        return leaveRequest;
    }
}