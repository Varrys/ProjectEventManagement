-- Insert initial data into the authors table                                                                                                       
INSERT INTO authors (first_name, last_name, birth_date)
VALUES ('Machado', 'de Assis', '1839-06-21'),
       ('Clarice', 'Lispector', '1920-12-10'),
       ('Jorge', 'Amado', '1912-08-10');

INSERT INTO books (title, publication_year, author_id, status)
VALUES ('Dom Casmurro', 1899, (SELECT id FROM authors LIMIT 1 OFFSET 0), 'Available'),
       ('The Hour of the Star', 1977, (SELECT id FROM authors LIMIT 1 OFFSET 1), 'Borrowed'),
       ('Gabriela, Clove and Cinnamon', 1958, (SELECT id FROM authors LIMIT 1 OFFSET 2), 'Available');

INSERT INTO users (email, username, password, name, role, createdAt, enable)
VALUES ('user1@example.com', 'user1', '123', 'User 1', 'Admin', NOW(), true),
       ('user2@example.com', 'user2', '234', 'User 2', 'User', NOW(), true),
       ('user2@example.com', 'user3', '345', 'User2 3', 'UserManager', NOW(), true);

INSERT INTO events (name, date, location, description, maxCapacity, userid)
VALUES ('Event 1', NOW(), 'Location 1', 'Description 1', 100, (SELECT userId FROM users LIMIT 1 OFFSET 0)),
       ('Event 2', NOW(), 'Location 2', 'Description 2', 200, (SELECT userId FROM users LIMIT 1 OFFSET 1)),
       ('Event 3', NOW(), 'Location 3', 'Description 3', 300, (SELECT userId FROM users LIMIT 1 OFFSET 2));

INSERT INTO tickets (price, name, description, quantity, eventid)
VALUES (10.99, 'Ticket 1', 'Ticket Description 1', 50, (SELECT eventId FROM events LIMIT 1 OFFSET 0)),
       (20.99, 'Ticket 2', 'Ticket Description 2', 100, (SELECT eventId FROM events LIMIT 1 OFFSET 1)),
       (30.99, 'Ticket 3', 'Ticket Description 3', 150, (SELECT eventId FROM events LIMIT 1 OFFSET 2));

INSERT INTO userEvents (eventid, userid, feedback)
VALUES ((SELECT eventId FROM events LIMIT 1 OFFSET 0), (SELECT userId FROM users LIMIT 1 OFFSET 0), 'Feedback 1'),
       ((SELECT eventId FROM events LIMIT 1 OFFSET 1), (SELECT userId FROM users LIMIT 1 OFFSET 1), 'Feedback 2'),
       ((SELECT eventId FROM events LIMIT 1 OFFSET 2), (SELECT userId FROM users LIMIT 1 OFFSET 2), 'Feedback 3');