CREATE TABLE Reports (
      reportId uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
      eventId uuid REFERENCES events (eventId,
      typeReport VARCHAR(50)  NOT NULL,
      valueTotal DECIMAL(10, 2) NOT NULL,
      participants INTEGER NOT NULL,                 
);