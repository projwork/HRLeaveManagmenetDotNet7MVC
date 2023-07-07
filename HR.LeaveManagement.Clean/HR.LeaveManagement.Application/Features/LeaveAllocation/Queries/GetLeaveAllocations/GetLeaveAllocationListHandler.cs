using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public GetLeaveAllocationListHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        // To Add later
        // - Get records for specific user
        // - Get allocations per employee

        var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
        var allocations = new List<LeaveAllocationDto>();

        foreach (var leaveAllocation in leaveAllocations)
        {
            var leaveAllocationDto = new LeaveAllocationDto()
            {
                Id = leaveAllocation.Id,
                LeaveTypeId = leaveAllocation.LeaveTypeId,
                NumberOfDays = leaveAllocation.NumberOfDays,
                Period = leaveAllocation.Period,
                LeaveType = new LeaveTypeDto()
                {
                    Id = leaveAllocation.LeaveType.Id,
                    Name = leaveAllocation.LeaveType?.Name,
                    DefaultDays = leaveAllocation.LeaveType.DefaultDays,
                }
            };
            allocations.Add(leaveAllocationDto);
        }

        return allocations;
    }
}