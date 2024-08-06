# Customization Objective: .devcontainer folder should always be able to be deleted/removed with no impact to the app!

# Initial Customization Notes for the Dev Container

- Changed database name to 'MathDatabase' for consistency.
- App updated to
    - gracefully connect to a db in container, specifically: App will use the DEFAULT_CONNECTION environment variable, then the appsettings.json
    - log swagger url for easy access
- When container is created (initialize.sh) script will:  Install/configure install dotnet tooling inside container
- When container is started (start.sh) script will:  Start the app and watch for changes inside container

# Addional comment:  the included scripts are helpful reminders of steps needed for setup in a new environment (i.e. host machine, CI/CD, etc.)
