#!/bin/bash

# Function to handle errors
handle_error() {
    echo "Error occurred in script at line: $1"
    exit 1
}

# Trap errors and call handle_error function
trap 'handle_error $LINENO' ERR

# Update dotnet workload
echo ''
echo 'Updating/installing dotnet tools...'
echo ''

sudo dotnet workload update

# Install dotnet-ef tool globally
dotnet tool install --global dotnet-ef

# Update the database
echo ''
echo 'Initialize the db'
echo ''

dotnet ef database update  --project BackEndDevChallenge
