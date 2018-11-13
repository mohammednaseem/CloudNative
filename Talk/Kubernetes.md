# Creating Kubernetes Cluster in Azure

###intialize 
$resourceGroupName ='naseem'
$location = 'southeastasia'
$aksnodesize = 'Standard_DS1_v3'

###Login
Connect-AzureRmAccount
az login --use-device-code


######### Common functions ##########
Function GenerateAName(
    [string]$Prefix) {
    #Write-Host "GenName :",$Prefix;
    $random = (New-Guid).ToString().Substring(0, 5)
    $servicename = "$envName$Prefix$random"
    #Write-Host "Generated Name :",$servicename;

    return $servicename
}
######### Common functions ##########

######### Some Helpfule PS Commands #########
#az account list --output table # if you have multiple subscription - this commands gets me all active subscriptions
#az account set --subscription "15cxx6af-6xx9-40x7-ax60-xf5xxxxx62fx" #now set it to one of the active subscriptions

az aks get-versions -l $location # this returns all aks version for that specific location.. and i will go with highest
######### Some Helpfule PS Commands #########


$aksName =  'DHLM-Vehicles-' + (GenerateAName 'Aks-')

## Create the cluster
az aks create -g $resourceGroupName -n $aksName --generate-ssh-keys --node-count 1 --node-vm-size 'Standard_D1_v2' -k '1.11.3'
