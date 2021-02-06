USE saaanr;
-- NOTE all tables can have duplicate rows as there is only one primary key on id which does not guarantee uniqueness other columns...

CREATE TABLE profiles (
    id int NOT NULL AUTO_INCREMENT,
    firstname VARCHAR(255) NOT NULL,
    lastname VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    picture VARCHAR(255) NOT NULL,
    phone VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    member tinyint DEFAULT 0,
    INDEX userId (userId),  
    PRIMARY KEY (id)
);

CREATE TABLE vaults (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    INDEX userId (userId),
    PRIMARY KEY (id)
);

CREATE TABLE keeps (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    img VARCHAR(255),
    isPrivate TINYINT,
    views INT DEFAULT 0,
    shares INT DEFAULT 0,
    keeps INT DEFAULT 0,
    INDEX userId (userId),
    PRIMARY KEY (id)
);

CREATE TABLE vaultkeeps (
    id int NOT NULL AUTO_INCREMENT,
    vaultId int NOT NULL,
    keepId int NOT NULL,
    userId VARCHAR(255) NOT NULL,

    PRIMARY KEY (id),
    INDEX (vaultId, keepId),
    INDEX (userId),

    FOREIGN KEY (vaultId)
        REFERENCES vaults(id)
        ON DELETE CASCADE,

    FOREIGN KEY (keepId)
        REFERENCES keeps(id)
        ON DELETE CASCADE
)

-- -- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT 
--       k.*,
--       vk.id as vaultKeepId
-- FROM vaultkeeps vk
--       INNER JOIN keeps k ON k.id = vk.keepId 
-- WHERE (vaultId = @vaultId AND vk.userId = @userId) 

-- SELECT
--       k.*,vk.keeps
-- FROM keeps k
--       LEFT JOIN (
--         SELECT keepId,COUNT(*) as keeps
--             FROM `saaanr`.`vaultkeeps` 
--             GROUP BY keepId) vk ON k.id = vk.keepId
-- WHERE isPrivate = 0;

-- GET KEEPS BY USER
        -- SELECT 
        --   * 
        -- FROM keeps k
        --   LEFT JOIN (
        --     SELECT keepId,COUNT(*) as keeps
        --     FROM `saaanr`.`vaultkeeps` 
        --     GROUP BY keepId) vk ON k.id = vk.keepId
        -- WHERE userId = "auth0|5ede8f4b56d062001333e194";

-- SELECT * FROM keeps WHERE id >= 250;
-- DELETE FROM keeps WHERE id = 247;
--  SELECT 
--       vk.*
--  FROM vaultkeeps vk
--       INNER JOIN keeps k ON k.id = vk.keepId
--       INNER JOIN vaults v on v.id = vk.vaultId
--  WHERE (vaultId = 112);

-- GET KEEPS BY VAULT with autokeep count
-- SELECT
--       vk.id as vaultKeepId,
--       vk.vaultId as vaultId,
--       k.id as keepId,
--       k.userId as userId,
--       k.name as name,
--       k.description as description,
--       k.img as img,
--       k.isPrivate as isPrivate,
--       k.views as views,
--       k.shares as shares,
--       kc.keeps as keeps
-- FROM vaultkeeps vk
--       LEFT JOIN (
--             SELECT 
--             keepId,COUNT(*) as keeps
--             FROM `saaanr`.`vaultkeeps` 
--             GROUP BY keepId
--       ) kc ON vk.keepId = kc.keepId
--       INNER JOIN keeps k ON k.id = vk.keepId
-- WHERE (vk.vaultID = 102);


-- -- USE THIS TO CLEAN OUT YOUR DATABASE
-- DROP TABLE IF EXISTS vaultkeeps;
-- DROP TABLE IF EXISTS vaults;
-- DROP TABLE IF EXISTS keeps;
-- DROP TABLE IF EXISTS users;


-- Keeps:
-- DELETE FROM keeps WHERE name LIKE 'PRIVATE%';
-- UPDATE keeps SET views = views + 1 WHERE id = 235;
-- UPDATE keeps SET img='https://photos.app.goo.gl/goYsdDxfHSDXKCTh7' WHERE id = 251;

UPDATE keeps SET userID="auth0|5ede8f4b56d062001333e194"
WHERE id > 0;

UPDATE keeps SET isPrivate = 0 WHERE id >= 1;

SELECT * FROM keeps WHERE id>=0;

-- select * from `saaanr`.`keeps` order by id desc limit 100;

-- SELECT * FROM `saaanr`.`keeps` LIMIT 100;
-- UPDATE keeps SET userId="vraY84SbrIwTT3dUevCymVza5Xl47kLc@clients" WHERE id = 10;
-- {
--     "name": "Cookie Monster",
--     "description": "Lovable Cookie Dude!",
--     "img": "https://cnet3.cbsistatic.com/img/YuLvQxpo04T02vD5Zp-Cogdim2g=/1200x675/center/top/2019/07/26/26589aa5-6aee-48be-bb2a-2505e411d834/cookie.jpg",
--     "isPrivate": true,
--     "views": 0,
--     "shares": 0,
--     "keeps": 0
-- }
-- {
--     "name": "Somewhere Over the Rainbow",
--     "description": "You're not in Kansas anymore!",
--     "img": "https://images.unsplash.com/photo-1565073182887-6bcefbe225b1?ixlib=rb-1.2.1&w=1000&q=80",
--     "isPrivate": false,
--     "views": 0,
--     "shares": 0,
--     "keeps": 0
-- }
-- {
--     "name": "Please don't delete me!",
--     "description": "Pretty pretty please?",
--     "img": "https://rlv.zcache.co.uk/unhappy_face_6_cm_round_badge-r43e2fbd4603f4c2c9ba5a71cc74b300b_k94rf_540.jpg?rlvnet=1",
--     "isPrivate": true,
--     "views": 0,
--     "shares": 0,
--     "keeps": 0
-- }

-- Vaults:
-- DELETE FROM vaults WHERE name LIKE '%VAULT%';
-- SELECT * FROM `saaanr`.`vaults` LIMIT 100;
-- UPDATE vaults SET userId="vraY84SbrI@clients" WHERE id = 22;
-- {
--     "id": 9,
--     "userId": "vraY84SbrIwTT3dUevCymVza5Xl47kLc@clients",
--     "name": "Super Funny Stuff",
--     "description": "These things make me laugh out loud!"
-- },
-- {
--     "id": 11,
--     "userId": "vraY84SbrIwTT3dUevCymVza5Xl47kLc@clients",
--     "name": "Stuff to delete!",
--     "description": "Please delete this stuff."
-- },
-- {
--     "id": 23,
--     "userId": "vraY84SbrIwTT3dUevCymVza5Xl47kLc@clients",
--     "name": "More stuff to delete!",
--     "description": "Please delete this stuff."
-- },
-- {
--     "id": 28,
--     "userId": "vraY84SbrIwTT3dUevCymVza5Xl47kLc@clients",
--     "name": "Funny Stuff",
--     "description": "These things make me smile!"
-- }

-- VaultKeeps:
-- DELETE FROM vaultkeeps WHERE id > 22 && id < 60;
-- DELETE FROM keeps;
-- delete from vaults;
-- SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 222;


-- SELECT * FROM `saaanr`.`vaultkeeps` ORDER BY keepId ASC LIMIT 100 ;


-- select * from `saaanr`.`vaults` order by id desc LIMIT 100;
-- SELECT * FROM `saaanr`. `vaultkeeps` WHERE userId = "dont trust the front end";

-- SELECT * from `saaanr`.`vaultkeeps` WHERE vaultId = 11;


-- CleanUp:
-- DELETE FROM keeps WHERE name LIKE '%KEEP%';
-- DELETE FROM vaults where name LIKE '%VAULT';


-- CREATE
      -- INSERT INTO vaultkeeps
      --   (vaultId, keepId, userId)
      -- VALUES
      --   (112, 241, "auth|4ede8f4b56d062001333e194");
      
      -- UPDATE keeps
      -- SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 241)
      -- WHERE id = 241;

-- DELETE
      -- DELETE FROM VaultKeeps WHERE id = 298;

      -- UPDATE keeps
      -- SET keeps = (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 241)
      -- WHERE id = 241;

-- TESTING
  -- delete VK Id

  -- UPDATE keeps
  -- SET keeps = 
  --   (SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 
  --     (SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = @Id))
  -- WHERE id = (SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = @Id);


-- @Id = 308
--   UPDATE keeps
--   SET keeps = 
--      NumKeeps in VK where 
          -- Keep ID = 241
--   WHERE id = (SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = 308);


-- SELECT * FROM `saaanr`.`vaultkeeps` WHERE keepId = 241;
-- SELECT * FROM `saaanr`.`keeps` WHERE id = 241;

-- SELECT COUNT(*) FROM `saaanr`.`vaultkeeps` WHERE keepId = 241;
-- SELECT keepId FROM `saaanr`.`vaultkeeps` WHERE id = 308;


-- PREVENT DUPLICATE VAULTKEEPS
--  SELECT * FROM `saaanr`.`vaultkeeps` ORDER BY id DESC LIMIT 100
-- SELECT
--   userId, COUNT(userId),
--   vaultId, COUNT(vaultId),
--   keepId, COUNT(keepId)
-- FROM
--   vaultkeeps
-- GROUP BY
--   vaultId,
--   keepId
-- HAVING COUNT(userId) > 1
--   AND COUNT(vaultId) > 1
--   AND COUNT(keepId) > 1;

      INSERT INTO profiles
      (firstname, lastname, email, picture, phone, userId, member)
      VALUES
      ("Justin", "Mattson", "toyvo252@hotmail.com", "", "2084240740", 1, 0);
      SELECT LAST_INSERT_ID();