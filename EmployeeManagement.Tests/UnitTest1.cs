namespace EmployeeManagement.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void Employee_Name_Should_Not_Be_Empty()
        {
            // Arrange
            string employeeName = "John";

            // Act
            bool result = !string.IsNullOrEmpty(employeeName);

            // Assert
            Assert.True(result);
        }
    }
}