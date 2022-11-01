namespace Architecture.Tests
{
    public class ArchitectureTests
    {
        private const string DomainNamespage = "PraeceptorCQRS.Domain";
        private const string ApplicationNamespage = "PraeceptorCQRS.Application";
        private const string InfrastructureNamespage = "PraeceptorCQRS.Infrastructure";
        private const string PresentationNamespage = "PraeceptorCQRS.Presentation";
        private const string WebNamespage = "Web";
        /*
                [Fact]
                public void Domain_Should_Not_HaveDependencyOnOtherProjects()
                {
                    // Arrange
                    var assembly = typeof(Domain.AssemblyReference).Assembly;

                    var otherProjects = new[] {
                        ApplicationNamespage,
                        InfrastructureNamespage,
                        PresentationNamespage,
                        WebNamespage
                    };

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .ShouldNot()
                        .HaveDependencyOnAll(otherProjects)
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }

                [Fact]
                public void Application_Should_Not_HaveDependencyOnOtherProjects()
                {
                    // Arrange
                    var assembly = typeof(Application.AssemblyReference).Assembly;

                    var otherProjects = new[] {
                        InfrastructureNamespage,
                        PresentationNamespage,
                        WebNamespage
                    };

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .ShouldNot()
                        .HaveDependencyOnAll(otherProjects)
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }

                [Fact]
                public void Handlers_Should_Have_DependencyOnDomain()
                {
                    // Arrange
                    var assembly = typeof(Application.AssemblyReference).Assembly;

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .That()
                        .HaveNameEndingWith("Handler")
                        .Should()
                        .HaveDependencyOn(DomainNamespage)
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }

                [Fact]
                public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
                {
                    // Arrange
                    var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

                    var otherProjects = new[] {
                        PresentationNamespage,
                        WebNamespage
                    };

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .ShouldNot()
                        .HaveDependencyOnAll(otherProjects)
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }

                [Fact]
                public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
                {
                    // Arrange
                    var assembly = typeof(PraeceptorCQRS.Presentation.AssemblyReference).Assembly;

                    var otherProjects = new[] {
                        InfrastructureNamespage,
                        WebNamespage
                    };

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .ShouldNot()
                        .HaveDependencyOnAll(otherProjects)
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }

                [Fact]
                public void Controllers_Should_HaveDependencyOnMediatR()
                {
                    // Arrange
                    var assembly = typeof(Presentation.AssemblyReference).Assembly;

                    var otherProjects = new[] {
                        InfrastructureNamespage,
                        WebNamespage
                    };

                    // Act
                    var testResult = Types
                        .InAssembly(assembly)
                        .That()
                        .HaveNameEndingWith("Controller")
                        .Should()
                        .HaveDependencyOn("MediatR")
                        .GetResult();

                    // Assert
                    testResult.IsSuccessful.Should().BeTrue();
                }
        */
    }
}
