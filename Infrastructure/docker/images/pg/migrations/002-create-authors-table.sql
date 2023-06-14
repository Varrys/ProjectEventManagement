CREATE TABLE authors (
     id             uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
     first_name     VARCHAR(100) NOT NULL,
     last_name      VARCHAR(100) NOT NULL,
     birth_date     DATE NOT NULL
);