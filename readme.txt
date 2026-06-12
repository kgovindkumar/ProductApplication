Docker -v to check version
Create a image
docker build -t productapp:v1

To run a container with an image
docker run -d -p 8080:8080 --name productapp-container productapp

To check logs
docker log


Docker Compose
docker compose up --build
docker compose down


To run registry locally 
first you need to get the image of registry
docker run -d -p 5000:5000 --name productapp-registry registry:2

to push the image to this registry need to run below code
docker push localhost:5000/productapp:v2


to pull the image from your registry or from anyother registry
docker push localhost:5000/productapp:v2

to take backup
docker run --rm -v productapplication_sql-data:/data -v $(pwd):/backup alpine tar czf /backup/sql-data-backup.tar.gz -C /data .

to restore backup
docker run --rm -v productapplication_sql-data:/data -v $(pwd):/backup alpine sh -c "cd /data && tar xzf /backup/sql-data-backup.tar.gz"