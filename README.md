
# FetchRewards
Response to hiring prompt: https://github.com/fetch-rewards/receipt-processor-challenge.
Author: Sean Martens
Tech stack: ASP.NET, Docker, Swagger.

**Building the project**

 1. Pull down this repo.
 2. Navigate to the 'FetchDevSample' directory in command prompt
 3. Run command $docker build -t sean_fetch_api
 4. Make sure the image was created by running $docker images
 5. Run the image from Docker Hub, map the two requested ports, I used 3301:443 and 3302:80.
 6. Interact with the API with your testing suite or Postman. The new receipt processor endpoint will be: http://localhost:3334/receipts/process
