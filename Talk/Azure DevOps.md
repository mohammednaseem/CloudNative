# Azure DevOps
If you are a Microservice Owner the following documentation shows you how to setup CI with Azure DevOps.
### Prerequisite: 
1) You should have an account with Azure DevOps. 

## Setup: 
1)	Log in to your Azure DevOps account.
2)	Select the correct Organization if there are more than 1.
3)	Create a new Project if an appropriate one does not exist.  
    For example in below screenshot I have given the name of the Microservice as the name of the Project. i.e.Device Management
 	4)	Create a new Build Pipeline
 
5)	Select the “Visual designer to create a pipeline without YAML”
 	6)	Select Bitbucket Cloud as source.

 

Type .NET Core in the Select a template textbox and “Apply” ASP.NET Core template

 

From the Agent Job remove the “Restore”, “Build”, “Publish” and leave only Test job in place. 
Hit the “Save and Queue” option. This will save the current configuration and queue up a Test. Any errors should be corrected
 

On successful configuration you should see an output like below.

 
Continue editing the configuration
 

Add a task to the agent job
 

Enter docker in the Add tasks textbox Add the Docker task
 
You should see a screen like below. 
There may already be already a connection to Azure Container Registry. If it is not there do the following steps,
 

Click the “Manage” link.
 
Select the Docker Registry
 
Now go back to the Original windows

 

Choose Container Registry and acr connection. 
Remember the above few steps are required only if the ACR connection is not already there. Some other developer may have created this before you, In that case do not create a duplicate connection.

 
Now configure “Image name” as shown above. 
Hit “Save and queue” build. This will start a new build. Clicking the build number as shown below will give you live logs.

 

Next add a new task to the agent job as shown below 
 
Choose Docker and then select “tag” from Command drop down.
 
 

Now add one more task to the pipeline and set command to “Push”
 
