CREATE TABLE users (
   userId    uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
   email     VARCHAR(255) NOT NULL,
   username  VARCHAR(100) NOT NULL,
   password  VARCHAR(255) NOT NULL,
   name      VARCHAR(100) NOT NULL,
   role      VARCHAR(50) NOT NULL,
   createdAt TIMESTAMP DEFAULT NOW(),
   enable    BOOLEAN NOT NULL
);