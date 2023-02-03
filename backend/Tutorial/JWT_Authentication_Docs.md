# JWT (JSON Web Token)

- It is an open standard for securely transmitting information between parties as a JSON object

- The purpose of using JWTs is not to hide the data but to ensure the authenticity of the data.

# When to use JWT?

- Authorization:
    - Once the user is logged in, each subsequent request will include the JWT, allowing the user to access routes and services that are permitted to that token.

- Information Exchange:
    - Because JWTs can be signed, you can be sure senders are who they say they are.
    

## Example
- User signs-in using username and password
- Authentication server verifies the credentials and issues a jwt signed using either a secret salt or private key
- User's Client uses the JWT to access protected resources by passing the JWT in HTTP Authorization header
- Resource server then verifies the authenticity of the token using the secret salt / public key