using Autofac.Extras.Moq;
using FidsCodingAssignment.Common.Models;
using FidsCodingAssignment.Core.Models;
using FidsCodingAssignment.Core.UnitTests.TestData;
using Microsoft.Extensions.Options;

namespace FidsCodingAssignment.Core.UnitTests.Services;

public abstract class ServiceBaseTests
{
    protected AutoMock GetMock()
    {
        var mock = AutoMock.GetLoose();
        var mockFlightConfigOptions = mock.Mock<IOptions<FlightConfiguration>>();
        mockFlightConfigOptions
            .Setup(x => x.Value)
            .Returns(FlightData.FlightConfig);

        return mock;
    }
}