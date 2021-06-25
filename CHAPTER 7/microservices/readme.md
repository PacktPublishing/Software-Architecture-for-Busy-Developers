Microservices Example
============
The instructions of this readme are the required steps to test the sample microservice application.
# K8s
You can use any flavor of K8s. For non-Windows users, you can use [minikube](https://minikube.sigs.k8s.io/docs/start/). Windows and Mac users can use [Docker Desktop](https://www.docker.com/products/docker-desktop).
You can of course rely on any cloud-based cluster. Make sure you create the application namespace.

`kubectl create ns microserviceapp`
# Installing Dapr
Once you have a vanilla K8s cluster up and running, you can install Dapr.
## Dapr CLI
Install the [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli)
## Install Dapr in the cluster
`dapr init --runtime-version 1.0.1 -k`

Note that I used the runtime version 1.0.1. Feel free to use a more recent runtime but if you want to run the example in the same context, use 1.0.1
# Installing RabbitMQ and retrieving the default password
To be cloud-neutral, I used a self-hosted RabbitMQ instance. To install it in your cluster:

`helm repo update` 

`helm install stable/rabbitmq --namespace microserviceapp`

To retrieve the default password, run the following commands:

`$pwd=kubectl get secret rabitmq-rabbitmq -n microserviceapp -o jsonpath="{.data.rabbitmq-password}"`
`[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($pwd))`

I am using PowerShell in this example. The first command line makes use of kubectl to retrieve the password in base64 value. The second line decodes the base64 value. Feel free to use any other way to decode the value.

# Deploying the sample app
Before deploying the app using deploy.yml, make sure to update:

`amqp://user:pwd`

with:

`amqp://user:password retrieved from the previous step`

Now, you can deploy the app to the cluster:

`kubectl apply -f deploy.yml`

# Testing the sample app
Verify that the application pods are running:

`kubectl get pod -n microserviceapp`

To place an order, you must forward the order processing (or any other Dapr-injected pod) traffic to the host:

`kubectl -n microserviceapp port-forward [orderprocessingpod] 3500:3500`

Using your preferred tool (Postman, Fiddler, cUrl, whatever), run the following HTTP POST request to place an order:

`curl -d '{"Id":"4aadc0f8-eeda-4ee7-9c26-a6d39cbfbc28","Products":[{"Id":"5678f982-2ae4-408c-92ff-6af45118d159"}]}' -H 'Content-Type: application/json' http://localhost:3500/v1.0/invoke/orderprocessing/method/order`

Verify that the order was processed correctly by checking the service logs

`kubectl -n microserviceapp logs [your-orderprocessing-pod] orderprocessing`
`kubectl -n microserviceapp logs [your-shippingprocessing-pod] shippingprocessing`

If everything went fine, you should see both services reporting about the newly placed order.
