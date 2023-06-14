CREATE TABLE books (
    id                  uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    title               VARCHAR(255) NOT NULL,
    publication_year    INTEGER NOT NULL,
    author_id           uuid REFERENCES authors(id),
    status              VARCHAR(20) NOT NULL
);
