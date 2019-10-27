# Learning Dotnet, Dockers & Kubernetes in Azure using a Real Project
#########################

## Cloud Native Vehicle Monitoring Platform
DHLM is a major (fictitious) courier company. They have 1000s of Delivery vehicles on the road every day delivering packages and the company wants to build a Vehicle Monitoring Solution. They have approached us to come up with an architecture and a PoC for their DHLM Management.

## Requirement is to provide a
1. #### UI where end user for any specific vehicle and finds it’s
+	   Current location
+	   Previous Locations
+	   Any overspending.
+	   Show the engine on status and engine poweron duration
2. #### UI where end user can choose and date and see
+	   All vehicles that were overspending.

## Platform Assumption
DHLM has chosen Azure Cloud as the platform where this solution has to be built on and deployed.

## Propose Architecture
![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/architecture.png "Architecture")

# Course Content (Training for new team members)

| Day           | Topic           | Design    | Lab  |
| ------------|:-------------|:------:|:-----:|
| 1      | **Microservices** Here we will be talk about Microservices and how they should be designed. We will use the DHLM use case and review the above Architecture.  | Yes   |  |
| 1| VMs & Dockers  |   Yes  |  |
| 1 | Installing Perquisites for the course |  | Yes |
| 1 | **Building the DHLM Vehicle Management Docker** The instructor choice of tool is .NET and we will be using VS Code to build the .NET Core application. Instructor will show how a Docker is built. The attendees will be broken into teams and they will follow along and built their own Vehicle Management Docker. |  | Yes |
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/food.png "Food")|||
| 1 | Deep dive on various Docker Commands.|  | Yes |
| 1 | **Pushing Docker Image to Private Registry.** We will be using Azure Container Registry (ACR) as our Private Registry. We will see how it is created and various features within it. The team will push the Docker they have created to the ACR. | x | Yes |
| 1 | **Azure Container Instance** We will look at this service. The ACI service lets us run individual instances of Docker images in the cloud |  | Yes |
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/sleep.png "Sleep")|||
| 2 | **Kubernetes Container Orchestration** We will talk about the problem it solves and go over belwo K8 primitives <ul><li>Masters</li><li>Nodes</li><li>Desired State and the Declarative Model</li><li>Pods</li><li>Services</li><li>Deployment</li></ul> | Yes | |
| 2 | Managed Kubernetes environment using Azure Kubernetes Services  | x | Yes |
| 2 | Deploying “Driver Management” Docker (built on Day 1) to the newly created Kubernetes cluster.| x | Yes |
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/food.png "Food")|||
| 2 | **Lab** <ul><li>Nodes</li><li>Desired State and the Declarative Model</li><li>Pods</li><li>Services (Nodeport, ClusterIP, LoadBalancer)</li><li>Deployments</li></ul> | | |
| 2 | **ConfigMaps & Secrets** | | |
| 2 | **NameSpaces** | | |
| 2| **Kubernetes Rolling Updates** Now we will simulate a situation where business comes to us with a Change Request. Also it is a requirement for us to Update production with zero downtime.
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/sleep.png "Sleep")|||
| 3 | **Complex Event Processing** We will talk about Event Driven Design and how Complex Event Processing System can be applied on a stream of Data or events to infer a state defined by Business. We will talk about how this is different from Model driven design. Identifying/recognizing the pattern is important to enable us to apply the right technology for a given problem. | Yes |  x |
| 3  | **Azure Stream Analytics** We will have sensor data streaming in from DHLM vehicles. This is a big data problem because we have to deal with huge velocity, volume and variety of sensor data. We will have a lab on how Azure Stream Analytics (a Complex Event Processing platform) helps us in processing this huge stream. | | Yes |
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/food.png "Food")|||
| 3 | **Event Management Docker** We will need one more Docker/Microservice to do some further labs. | | Yes |
| 3 | **Orchestration vs Choreography** We will talk about some design patterns. Will cover how API Management can help in Orchestrating Microservices and Service Bus can help in integration. The decision points. <ul><li>Strong Consistency vs Eventual Consistency </li><li>CAP Theorem</li></ul> | Yes | |
| 3 | **API Management** Microservice Orchestration | | Yes |
||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/sleep.png "Sleep")|||
| 4 | **Service Bus** Microservice Choreography | | Yes|
| 4 | **NGINX Ingress Controller** Ingress <ul><li>Ingress</li><li>Ingress Controller</li><li>Ingress Service</li></ul> | | 
  ||![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/food.png "Food")|||
| 4 | **Revisit Architecture to talk about Security** | Yes | |
| 4 | **Completing DHLM PoC** - The Device Management Microservice will expose APIs exposed through APIM and Kubernetes Ingress to give DHLM Vehicle information. All the vehicle business rules would be processed with Stream Analytics. This will be streamed to Event Management Microservice and finally to a Database. | | |
| 4 | **Azure DevOps** https://azure.microsoft.com/en-in/solutions/architecture/cicd-for-containers/   | * | Yes |
  
## After Setting Up CI/CD
![alt text](https://github.com/mohammednaseem/CloudNative/blob/master/images/cicd.png "Continous Integration & Deployment")
  
