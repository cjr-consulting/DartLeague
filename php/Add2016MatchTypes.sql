INSERT INTO match_types ( Name ) VALUES ('A Div Winter Match');
INSERT INTO match_types ( Name ) VALUES ('B Div Winter Match');
INSERT INTO match_types ( Name ) VALUES ('C Div Winter Match');

INSERT INTO match_type_game_rules (
  matchTypeId,
  gameType,
  doubleIn,
  doubleOut,
  orderId,
  bestOfNumberOfLegs,
  numberOfLegs,
  whoStarts,
  numberOfPlayers,
  gamePointValue,
  legPointValue,
  forfeitIfNoPlayers,
  groupName
)
SELECT 2,'cricket'      , 0, 0,  1, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'cricket'      , 0, 0,  2, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'cricket'      , 0, 0,  3, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'501'          , 0, 1,  4, 0, 1, 'A', 1, 1, 0, 1, '501'            UNION
SELECT 2,'501'          , 0, 1,  5, 0, 1, 'H', 1, 1, 0, 1, '501'            UNION
SELECT 2,'501'          , 0, 1,  6, 0, 1, 'A', 1, 1, 0, 1, '501'            UNION
SELECT 2,'doublecricket', 0, 0,  7, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 2,'doublecricket', 0, 0,  8, 0, 1, 'A', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 2,'doublecricket', 0, 0,  9, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 2,'double501'    , 0, 1, 10, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 2,'double501'    , 0, 1, 11, 0, 1, 'H', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 2,'double501'    , 0, 1, 12, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 2,'cricket'      , 0, 0, 13, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'cricket'      , 0, 0, 14, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'cricket'      , 0, 0, 15, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 2,'501'          , 0, 1, 16, 0, 1, 'A', 1, 1, 0, 1, '501'            UNION
SELECT 2,'501'          , 0, 1, 17, 0, 1, 'H', 1, 1, 0, 1, '501'            UNION
SELECT 2,'501'          , 0, 1, 18, 0, 1, 'A', 1, 1, 0, 1, '501'            UNION
SELECT 2,'801'          , 0, 1, 19, 0, 1,  '', 3, 3, 0, 1, '801'            UNION


SELECT 3,'cricket'      , 0, 0,  1, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'cricket'      , 0, 0,  2, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'cricket'      , 0, 0,  3, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'301'          , 1, 1,  4, 0, 1, 'A', 1, 1, 0, 1, '301'            UNION
SELECT 3,'301'          , 1, 1,  5, 0, 1, 'H', 1, 1, 0, 1, '301'            UNION
SELECT 3,'301'          , 1, 1,  6, 0, 1, 'A', 1, 1, 0, 1, '301'            UNION
SELECT 3,'doublecricket', 0, 0,  7, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 3,'doublecricket', 0, 0,  8, 0, 1, 'A', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 3,'doublecricket', 0, 0,  9, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 3,'double501'    , 0, 1, 10, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 3,'double501'    , 0, 1, 11, 0, 1, 'H', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 3,'double501'    , 0, 1, 12, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 3,'cricket'      , 0, 0, 13, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'cricket'      , 0, 0, 14, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'cricket'      , 0, 0, 15, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 3,'301'          , 1, 1, 16, 0, 1, 'A', 1, 1, 0, 1, '301'            UNION
SELECT 3,'301'          , 1, 1, 17, 0, 1, 'H', 1, 1, 0, 1, '301'            UNION
SELECT 3,'301'          , 1, 1, 18, 0, 1, 'A', 1, 1, 0, 1, '301'            UNION
SELECT 3,'701'          , 0, 1, 19, 0, 1,  '', 3, 3, 0, 1, '701'            UNION

SELECT 4,'cricket'      , 0, 0,  1, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'cricket'      , 0, 0,  2, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'cricket'      , 0, 0,  3, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'401'          , 0, 1,  4, 0, 1, 'A', 1, 1, 0, 1, '401'            UNION
SELECT 4,'401'          , 0, 1,  5, 0, 1, 'H', 1, 1, 0, 1, '401'            UNION
SELECT 4,'401'          , 0, 1,  6, 0, 1, 'A', 1, 1, 0, 1, '401'            UNION
SELECT 4,'doublecricket', 0, 0,  7, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 4,'doublecricket', 0, 0,  8, 0, 1, 'A', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 4,'doublecricket', 0, 0,  9, 0, 1, 'H', 2, 2, 0, 1, 'Double Cricket' UNION
SELECT 4,'double501'    , 0, 1, 10, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 4,'double501'    , 0, 1, 11, 0, 1, 'H', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 4,'double501'    , 0, 1, 12, 0, 1, 'A', 2, 2, 0, 1, 'Double 501'     UNION
SELECT 4,'cricket'      , 0, 0, 13, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'cricket'      , 0, 0, 14, 0, 1, 'A', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'cricket'      , 0, 0, 15, 0, 1, 'H', 1, 1, 0, 1, 'Cricket'        UNION
SELECT 4,'401'          , 0, 1, 16, 0, 1, 'A', 1, 1, 0, 1, '401'            UNION
SELECT 4,'401'          , 0, 1, 17, 0, 1, 'H', 1, 1, 0, 1, '401'            UNION
SELECT 4,'401'          , 0, 1, 18, 0, 1, 'A', 1, 1, 0, 1, '401'            UNION
SELECT 4,'601'          , 0, 1, 19, 0, 1,  '', 3, 3, 0, 1, '601'