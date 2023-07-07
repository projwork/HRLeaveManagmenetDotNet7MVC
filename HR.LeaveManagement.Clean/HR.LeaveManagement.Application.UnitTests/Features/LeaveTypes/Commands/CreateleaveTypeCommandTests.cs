using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands;

public class CreateleaveTypeCommandTests
{
    private Mock<ILeaveTypeRepository> _mockRepo;

    public CreateleaveTypeCommandTests()
    {
        _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();
    }

    [Fact]
    public async Task Handle_ValidLeaveType()
    {
        var handler = new CreateLeaveTypeCommandHandler(_mockRepo.Object);

        await handler.Handle(new CreateLeaveTypeCommand()
        {
            Name = "Test1",
            DefaultDays = 1
        }, CancellationToken.None);

        var leaveTypes = await _mockRepo.Object.GetAsync();
        leaveTypes.Count.ShouldBe(4);
    }
}