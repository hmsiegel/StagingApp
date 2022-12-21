namespace StagingApp.Application.UnitTest.Shell.Queries.DetermineDevice;
public class DetermineDeviceQueryHandlerTests
{
    private readonly Mock<IDeviceTypeService> _deviceServiceMock;

    public DetermineDeviceQueryHandlerTests(Mock<IDeviceTypeService> deviceServiceMock)
    {
        _deviceServiceMock = deviceServiceMock;
    }

    [Fact]
    public async Task Handler_ShouldReturnFailure_WhenDeviceTypeIsNotKnown()
    {
        // Arrange
        var query = new DetermineDeviceQuery();

        // Act

        // Assert
    }
}
