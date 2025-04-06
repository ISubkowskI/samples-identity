# Identity
Projects (.net c#) with examples for identity

# ae-sample-identity-webapp
Docker
	ae-sample-identity-webapp.Dockerfile
Usage:
	- Build the image
		docker build -t aesamples/ae-sample-identity-webapp -f ae-sample-identity-webapp.Dockerfile . # in the same folder as the Dockerfile
	- Run the image
		docker run -it --rm -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp
		docker run -d -p 8000:8080 --name ae-sample-identity-webapp aesamples/ae-sample-identity-webapp
	- Open the browser and navigate to http://localhost:8000
	- Login with the following credentials:
		Email: info@softaren.com Password: Demo
		Email: notifications@softaren.com Password: Demo
	