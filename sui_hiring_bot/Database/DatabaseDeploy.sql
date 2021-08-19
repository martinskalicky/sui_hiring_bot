CREATE TABLE IF NOT EXISTS target_ships(
                                           ship_id INT NOT NULL,
                                           created_date_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
                                           updated_date_time TIMESTAMP NOT NULL DEFAULT  CURRENT_TIMESTAMP(3)
                                               ON UPDATE CURRENT_TIMESTAMP(3),
                                           ship_name NVARCHAR(255) NOT NULL,
                                           system_rule FLOAT,
                                           status VARCHAR(30),
                                           tracked BOOLEAN NOT NULL,
                                           UNIQUE KEY(ship_name)
);
CREATE TABLE IF NOT EXISTS job_table(
                                        ID INT NOT NULL AUTO_INCREMENT,
                                        created_date_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
                                        updated_date_time TIMESTAMP NOT NULL DEFAULT  CURRENT_TIMESTAMP(3)
                                            ON UPDATE CURRENT_TIMESTAMP(3),
                                        start_date DATETIME,
                                        end_date DATETIME,
                                        status VARCHAR(20),
                                        processed_players INT,
                                        PRIMARY KEY(ID)
);
CREATE TABLE IF NOT EXISTS input_players(
                                            ID INT NOT NULL AUTO_INCREMENT,
                                            created_date_time DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP(3),
                                            updated_date_time TIMESTAMP NOT NULL DEFAULT  CURRENT_TIMESTAMP(3)
                                                ON UPDATE CURRENT_TIMESTAMP(3),
                                            status VARCHAR(20) NOT NULL DEFAULT 'NEW',
                                            player_name NVARCHAR(255) NOT NULL,
                                            character_id INT NOT NULL,
                                            player_ship VARCHAR(30) NOT NULL,
                                            system_name VARCHAR(30) NOT NULL,
                                            PRIMARY KEY(ID),
                                            UNIQUE KEY(player_name)
);
CREATE TABLE IF NOT EXISTS email_template(
                                             ship_name NVARCHAR(255) NOT NULL,
                                             name VARCHAR(100) NOT NULL,
                                             variable_txt NVARCHAR(255),
                                             data TEXT
);
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('22544','Hulk', 1, 0.5,  'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('22546','Skiff', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('22548','Mackinaw', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('17478','Retriever', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('17476','Covetor', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('17480','Procurer', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('32880','Venture', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('37135','Endurance', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('33697','Prospect', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('42244','Porpoise', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('28606','Orca', 1, 0.5, 'ACTIVE');
INSERT IGNORE INTO target_ships(ship_id, ship_name, tracked, system_rule, status)
VALUES ('28352','Rorqual', 1, 0.5, 'ACTIVE');
