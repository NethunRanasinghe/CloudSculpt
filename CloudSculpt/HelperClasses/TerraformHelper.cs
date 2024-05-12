using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CliWrap;
using CliWrap.Buffered;
using CloudSculpt.ViewModels;

namespace CloudSculpt.HelperClasses;

public static class TerraformHelper
{
    private static async Task<string> InitializeTerraform()
    {
        var arguments = "init";
        var outputValue = await Cli.Wrap("terraform")
            .WithWorkingDirectory("./Configs")
            .WithArguments(arguments)
            .ExecuteBufferedAsync();

        return outputValue.StandardOutput;
    }
    
    public static async Task<bool> DeployToTerraform(ObservableCollection<ServiceElementViewModel> serviceElements)
    {
        try
        {
            // Delete the existing ec2_instances.tf file
            var filePath = "./Configs/ec2_instances.tf";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create a StringBuilder to hold the Terraform code
            StringBuilder sb = new StringBuilder();

            // Iterate over the service elements
            for (int i = 0; i < serviceElements.Count; i++)
            {
                // Append the Terraform code for an EC2 instance
                sb.AppendLine($"resource \"aws_instance\" \"example{i}\" {{");
                sb.AppendLine("  ami           = \"ami-06c4be2792f419b7b\"");
                sb.AppendLine("  instance_type = \"t2.micro\"");
                sb.AppendLine($"  tags = {{");
                sb.AppendLine($"    Name = \"{serviceElements[i].Text}\"");
                sb.AppendLine("  }");
                sb.AppendLine("}");
            }

            // Write the Terraform code to a new file
            await File.WriteAllTextAsync("./Configs/ec2_instances.tf", sb.ToString());

            // Initialize Terraform
            var initOutput = InitializeTerraform().Result;

            var applyOutput = await Cli.Wrap("terraform")
                .WithWorkingDirectory("./Configs")
                .WithArguments("apply -auto-approve")
                .ExecuteBufferedAsync();

            return true;
        }
        catch
        {
            return false;
        }
        
    }

}