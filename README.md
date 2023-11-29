# AppointmentBookingService

# This builds a docker image and a local repository called appointment-booking with the docker image
docker build command:
docker build -t appointment-booking -f Dockerfile .

Run the following to view docker images:
docker images

Run the following to run the docker image/ create a docker container. This command uses the image name as can be seen below.
docker create --name appointment-booking-container appointment-booking

Start the docker container:
docker start appointment-booking-container

Stopping the docker container:
docker start appointment-booking-container