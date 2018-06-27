CREATE TABLE smoothies (
  id int NOT NULL AUTO_INCREMENT,
  price DECIMAL,
  name VARCHAR(255),
  description VARCHAR(255),
  PRIMARY KEY (id)
);

SELECT * FROM smoothies WHERE name="Mango Madness"