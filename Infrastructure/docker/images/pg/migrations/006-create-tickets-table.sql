CREATE TABLE tickets
(
    ticketId    uuid DEFAULT uuid_generate_v4() PRIMARY KEY,
    price       DECIMAL(10, 2) NOT NULL,
    name        VARCHAR(100)   NOT NULL,
    description VARCHAR(255)   NOT NULL,
    quantity    INTEGER        NOT NULL,
    eventId     uuid REFERENCES events (eventId)
);