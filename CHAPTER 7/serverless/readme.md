Serverless Example
============
The instructions of this readme are the required steps to test the sample serverless application. Because serverless architectures are based on cloud offerings, I had to chose a cloud provider and went for Azure.
# K8s
If you already tested the microservices app, you can skip this step. Else, you can use any flavor of K8s. For non-Windows users, you can use [minikube](https://minikube.sigs.k8s.io/docs/start/). Windows and Mac users can use [Docker Desktop](https://www.docker.com/products/docker-desktop).
You can of course rely on any cloud-based cluster. Make sure you create the application namespace.

# Deploying the Azure infrastructure

- Make sure you have a valid Azure subscription (trial https://azure.microsoft.com/free/) and that you are at least contributor of the subscription. Once you have your subscription, login to https://shell.azure.com and accept the prompt if any.
- Download the IaC files and the zip package that contains the function code. Files are available in the [deploy folder](https://github.com/stephaneey/The-Hundred-Page-Software-Architecture-Book/tree/CHAPTER%207/serverless/deploy). Once downloaded, upload them to your Cloud Shell instance.

![image](https://user-images.githubusercontent.com/2558877/120927151-22938800-c6e0-11eb-8c80-0d930877ecda.png)

- Next, create the resource group hosting the resources

`az group create -l westeurope -n packtserverless`

- Now, it is time to deploy the different services. Run the following command to deploy the first template:

`az deployment group create --template-file serverless1.json --resource-group packtserverless --parameters appName="YOURVALUE"`

Make sure to replace YOURVALUE by something unique. This will be used by Azure to connect to the function. In my case, I used packtsrvlessarch. Make sure not to use any fancy character. At this stage, you should find the following resources in your resource group:

![image](https://user-images.githubusercontent.com/2558877/120927278-a77ea180-c6e0-11eb-94b4-304c038c598d.png)

- Now, you can deploy the function code that sits into the zip file

`az webapp deployment source config-zip --resource-group packtserverless --name YOURVALUE --src event-consumer.zip`

Now comes the last part of the deployment, which is the creation of the event grid topic and subscription. This last part will subscribe the previously deployed function to the topic.

`az deployment group create --template-file serverless2.json --resource-group packtserverless --parameters eventGridTopicName='YOURVALUEtp' eventGridSubscriptionName='YOURVALUEtpsub' functionAppName='YOURVALUE'`

Note that YOURVALUE still represents the name of your function. At this stage, you should have all resources deployed and linked together.

![image](https://user-images.githubusercontent.com/2558877/120927324-e01e7b00-c6e0-11eb-9345-ab72f95e2ffb.png)

 The last item is the event grid topic. When clicking on it, you should see that a subscription for the Azure function was indeed created.
 
 ![image](https://user-images.githubusercontent.com/2558877/120927404-02b09400-c6e1-11eb-8f1b-b1961c7b6282.png)

The name will be YOURVALUEtpsub. While you are looking at your event grid topic, take note of the topic endpoint and access keys. The topic endpoint can be grabbed from the overview page, and the access keys (2 keys) are accessible through the left menu. Copy only one of the keys. We need both the endpoint and one of the keys for our event publisher. The Azure infrastructure is completely deployed.

# Deploying the Event Publisher

- Edit [serverless.yml](https://github.com/stephaneey/The-Hundred-Page-Software-Architecture-Book/tree/CHAPTER%207/serverless/deploy/serverless.yml and replace YOURENDPOINT and YOURKEY with your own values, taken from the preceding step:
- Deploy the yml file to your cluster.

`kubectl apply -f .\serverless.yml`

# Testing the application

Now that everything has been deployed, you should have at least one instance of the event publisher pod running. You can verify this with a kubectl get pod command.

![image](https://user-images.githubusercontent.com/2558877/120927543-76eb3780-c6e1-11eb-9eee-db82757f02fa.png)

This should already publish events to our grid and notifications should be pushed to our function. Before we scale out to more instances, navigate to your Application Insights resource and click on the live metrics menu on the left. Once the metrics are showing up, scale out the event publisher, as shown below.

![image](https://user-images.githubusercontent.com/2558877/120927558-8cf8f800-c6e1-11eb-8b6a-f6305dd43deb.png)

I scaled event publisher to 50 instances. This may or may not work in your own environment, depending on your cluster capacity. If 50 is too much, just reduce it to 5. The goal is to create more events and see how Azure functions scale accordingly. The live metrics screen should show something similar to this:

![image](https://user-images.githubusercontent.com/2558877/120927569-9bdfaa80-c6e1-11eb-9807-aab0f2318424.png)

No matter how you scaled the event publisher, leave it running during about a minute and then scale the deployment back to zero. 
As you can see from figure xxx, at peak time, the system was handling about 525 requests per second and most executions took less than 20 milliseconds. We did not have to configure anything, we just let the cloud provider adjust the computing power according to the demand. The below image shows that the system scaled out, up to five instances.

![image](https://user-images.githubusercontent.com/2558877/120927586-ab5ef380-c6e1-11eb-8bee-7c341094a4e6.png)

Running the same query after stopping the event publisher, returns no results, meaning that there is no more instance running. You do not need to run the query yourself. So this very small serverless application is indeed based on a system that scales out and in according to the demand. You could force Azure functions to scale even more should you have a powerful cluster or run the event publisher from different systems at the same time.




