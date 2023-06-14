-- Insert initial data into the authors table                                                                                                       
INSERT INTO authors (first_name, last_name, birth_date)
VALUES ('Machado', 'de Assis', '1839-06-21'),
       ('Clarice', 'Lispector', '1920-12-10'),
       ('Jorge', 'Amado', '1912-08-10');

-- Insert initial data into the books table                                                                                                         
INSERT INTO books (title, publication_year, author_id, status)
VALUES ('Dom Casmurro', 1899, (SELECT id FROM authors LIMIT 1 OFFSET 0), 'Available'),
       ('The Hour of the Star', 1977, (SELECT id FROM authors LIMIT 1 OFFSET 1), 'Borrowed'),
       ('Gabriela, Clove and Cinnamon', 1958, (SELECT id FROM authors LIMIT 1 OFFSET 2), 'Available');                                              