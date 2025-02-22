﻿module JSONDefs

[<Literal>]
let tileset = """
{
    "name": "overworld",
    "columns": 40,
    "image": "Overworld_Tileset.png",
    "imageheight": 600,
    "imagewidth": 600,
    "margin": 0,
    "spacing": 0,
    "tileheight": 16,
    "tilewidth": 16,
    "type": "tileset",
    "terrains": [
        {
            "name": "test-layer-name",
            "tile": 150
        },
        {
            "name": "test-layer-name (2)",
            "tile": 999
        }
    ],
    "tilecount": 1500,
    "tiledversion": "1.3.4",
    "tiles": [
        {
            "id": 0,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "plains"
                }
            ],
            "terrain": [
                1,
                1,
                1,
                1
            ]
        },
        {
            "id": 1,
            "probability": 0.25,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "plains"
                }
            ],
            "terrain": [
                1,
                1,
                1,
                1
            ]
        },
        {
            "id": 2,
            "probability": 0.50,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "plains"
                }
            ],
            "terrain": [
                1,
                1,
                1,
                1
            ]
        },
        {
            "id": 31,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "river"
                }
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 32
                },
                {
                    "duration": 240,
                    "tileid": 28
                },
                {
                    "duration": 240,
                    "tileid": 24
                },
                {
                    "duration": 240,
                    "tileid": 20
                }
            ],
            "id": 32,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "river"
                }
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 33
                },
                {
                    "duration": 240,
                    "tileid": 29
                },
                {
                    "duration": 240,
                    "tileid": 25
                },
                {
                    "duration": 240,
                    "tileid": 21
                }
            ],
            "id": 33,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "river"
                }
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 34
                },
                {
                    "duration": 240,
                    "tileid": 30
                },
                {
                    "duration": 240,
                    "tileid": 26
                },
                {
                    "duration": 240,
                    "tileid": 22
                }
            ],
            "id": 34,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "river"
                }
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 35
                },
                {
                    "duration": 240,
                    "tileid": 31
                },
                {
                    "duration": 240,
                    "tileid": 27
                },
                {
                    "duration": 240,
                    "tileid": 23
                }
            ],
            "id": 35,
            "properties": [
                {
                    "name": "ttype",
                    "type": "string",
                    "value": "river"
                }
            ]
        },
        {
            "id": 481,
            "terrain": [
                4,
                4,
                1,
                1
            ]
        },
        {
            "id": 482,
            "terrain": [
                4,
                1,
                1,
                1
            ]
        },
        {
            "id": 483,
            "terrain": [
                1,
                4,
                4,
                1
            ]
        },
        {
            "id": 484,
            "terrain": [
                4,
                1,
                1,
                4
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 501
                },
                {
                    "duration": 240,
                    "tileid": 506
                },
                {
                    "duration": 240,
                    "tileid": 511
                },
                {
                    "duration": 240,
                    "tileid": 516
                },
                {
                    "duration": 240,
                    "tileid": 511
                },
                {
                    "duration": 240,
                    "tileid": 506
                }
            ],
            "id": 501,
            "terrain": [
                5,
                5,
                1,
                1
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 1442
                },
                {
                    "duration": 240,
                    "tileid": 1446
                },
                {
                    "duration": 240,
                    "tileid": 1450
                },
                {
                    "duration": 240,
                    "tileid": 1454
                },
                {
                    "duration": 240,
                    "tileid": 1450
                },
                {
                    "duration": 240,
                    "tileid": 1446
                }
            ],
            "id": 1442
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 1443
                },
                {
                    "duration": 240,
                    "tileid": 1447
                },
                {
                    "duration": 240,
                    "tileid": 1451
                },
                {
                    "duration": 240,
                    "tileid": 1455
                },
                {
                    "duration": 240,
                    "tileid": 1451
                },
                {
                    "duration": 240,
                    "tileid": 1447
                }
            ],
            "id": 1443
        },
        {
            "id": 1478,
            "terrain": [
                1,
                6,
                6,
                1
            ]
        },
        {
            "animation": [
                {
                    "duration": 240,
                    "tileid": 1480
                },
                {
                    "duration": 240,
                    "tileid": 1484
                },
                {
                    "duration": 240,
                    "tileid": 1488
                },
                {
                    "duration": 240,
                    "tileid": 1492
                },
                {
                    "duration": 240,
                    "tileid": 1488
                },
                {
                    "duration": 240,
                    "tileid": 1484
                }
            ],
            "id": 1480
        }
    ],
    "version": 1.2
}
"""

[<Literal>]
let tilemap = """
{
    "compressionlevel": -1,
    "editorsettings": {
        "export":
        {
            "format":"Godot",
            "target":"testmap.tscn"
        }
    },
    "height": 24,
    "infinite": false,
    "layers":[
        {
            "data":[0, 0, 822, 822, 822, 822, 822, 825, 862, 863, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 0, 0, 0, 822, 822, 822, 822, 822, 825, 863, 802, 802, 802, 802, 802, 802, 802, 802, 802, 821, 0, 0, 0, 0, 0, 0, 0, 822, 822, 822, 822, 825, 863, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 0, 862, 862, 824, 825, 862, 863, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 0, 802, 802, 861, 863, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 0, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 0, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 0, 0, 0, 0, 0, 0, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 781, 784, 0, 0, 0, 0, 0, 783, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 821, 822, 0, 0, 0, 0, 0, 0, 783, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 781, 784, 822, 0, 0, 0, 0, 0, 0, 0, 783, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 821, 822, 822, 0, 0, 0, 0, 0, 0, 0, 0, 783, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 821, 822, 822, 0, 0, 0, 0, 0, 0, 0, 0, 0, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 802, 781, 784, 822, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 802, 802, 802, 802, 802, 802, 802, 802, 781, 784, 822, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "height":24,
            "id":3,
            "name":"Tile Layer 3",
            "opacity":1,
            "type":"tilelayer",
            "visible":true,
            "width":24,
            "x":0,
            "y":0
        }, 
        {
            "data":[1, 1, 1, 1, 1, 2, 1, 1, 1, 501, 464, 462, 462, 702, 462, 463, 1, 1, 1, 161, 162, 162, 162, 162, 1, 1, 2, 1, 2, 1, 3, 1, 2, 1, 461, 702, 462, 462, 462, 425, 423, 1, 1, 201, 202, 202, 202, 202, 1, 1, 1, 1, 1, 1, 1, 421, 422, 422, 424, 702, 465, 502, 502, 464, 463, 1, 1, 201, 202, 202, 202, 202, 3, 1, 1, 1, 3, 1, 421, 424, 702, 462, 702, 462, 463, 1, 3, 461, 463, 1, 161, 1397, 202, 202, 202, 1438, 422, 423, 3, 3, 1, 2, 501, 119, 502, 464, 702, 465, 503, 2, 421, 504, 503, 1, 201, 202, 202, 202, 202, 203, 702, 425, 422, 423, 1, 2, 1, 61, 1, 461, 465, 503, 1, 3, 461, 463, 1, 1, 241, 1437, 202, 202, 202, 203, 462, 702, 702, 425, 423, 2, 1, 61, 1, 501, 503, 3, 2, 3, 461, 425, 422, 423, 1, 241, 242, 1437, 202, 203, 464, 702, 702, 702, 425, 423, 22, 116, 1, 2, 1, 1, 421, 422, 424, 702, 465, 503, 1, 1, 1, 241, 242, 243, 501, 502, 464, 462, 462, 425, 198, 423, 1, 1, 1, 421, 424, 702, 465, 502, 503, 1, 3, 1, 1, 1, 1, 1, 1, 1, 501, 464, 702, 702, 702, 425, 422, 423, 421, 424, 702, 462, 463, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 461, 462, 702, 462, 462, 462, 425, 424, 702, 702, 465, 503, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 501, 464, 702, 702, 462, 702, 462, 462, 702, 462, 463, 2, 1, 3, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 501, 502, 502, 464, 702, 702, 462, 702, 465, 503, 3, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 501, 502, 502, 502, 502, 503, 3, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
            "height":24,
            "id":1,
            "name":"Tile Layer 1",
            "opacity":1,
            "type":"tilelayer",
            "visible":true,
            "width":24,
            "x":0,
            "y":0
        }, 
        {
            "data":[0, 0, 0, 0, 327, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 321, 322, 0, 0, 0, 0, 0, 0, 327, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 287, 0, 0, 281, 284, 322, 327, 0, 0, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 281, 284, 322, 286, 0, 0, 327, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 135, 178, 180, 16, 0, 281, 284, 326, 322, 322, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53, 0, 0, 0, 53, 287, 321, 322, 325, 362, 362, 0, 0, 0, 0, 0, 287, 0, 98, 16, 0, 0, 0, 14, 96, 0, 0, 0, 94, 16, 361, 362, 363, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 53, 0, 0, 14, 96, 287, 0, 0, 0, 0, 97, 0, 0, 287, 327, 0, 0, 0, 0, 0, 0, 0, 0, 0, 94, 135, 135, 96, 0, 0, 0, 0, 0, 0, 53, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 570, 327, 0, 0, 0, 0, 0, 0, 0, 0, 137, 327, 327, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 287, 137, 0, 0, 0, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 287, 570, 570, 97, 0, 0, 0, 327, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 96, 0, 327, 0, 0, 0, 0, 0, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 327, 14, 96, 287, 0, 0, 0, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 14, 96, 0, 0, 0, 287, 0, 0, 0, 0, 0, 0, 490, 135, 135, 57, 135, 57, 57, 135, 135, 57, 135, 17, 96, 0, 287, 0, 0, 0, 0, 287, 0, 0, 0, 0, 0, 0, 0, 287, 0, 0, 0, 327, 0, 0, 0, 0, 0, 0, 281, 282, 282, 283, 0, 327, 0, 0, 287, 0, 0, 327, 0, 0, 0, 327, 0, 0, 0, 0, 0, 287, 287, 281, 284, 326, 286, 323, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 281, 282, 282, 282, 282, 283, 287, 321, 286, 322, 286, 323, 0, 0, 0, 0, 0, 281, 282, 282, 283, 0, 327, 0, 321, 322, 286, 322, 326, 285, 282, 284, 326, 286, 325, 363, 0, 0, 327, 0, 0, 321, 326, 322, 285, 283, 0, 0, 321, 322, 322, 322, 322, 322, 286, 326, 322, 326, 323, 0, 0, 327, 0, 0, 0, 361, 324, 326, 326, 323, 0, 0, 361, 362, 324, 286, 286, 322, 322, 286, 286, 325, 363, 0, 287, 0, 0, 0, 327, 0, 361, 362, 362, 363, 0, 0, 0, 0, 361, 362, 362, 362, 324, 286, 325, 363, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 327, 0, 0, 0, 0, 0, 361, 362, 363, 0, 0, 287, 0, 287, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 327, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
            "height":24,
            "id":2,
            "name":"Tile Layer 2",
            "opacity":1,
            "type":"tilelayer",
            "visible":true,
            "width":24,
            "x":0,
            "y":0
        }],
    "nextlayerid":4,
    "nextobjectid":1,
    "orientation":"orthogonal",
    "properties":[
        {
            "name":"projectRoot",
            "type":"string",
            "value":"..\/"
        }],
    "renderorder":"right-down",
    "tiledversion":"1.3.4",
    "tileheight":16,
    "tilesets":[
        {
            "firstgid":1,
            "source":"OverworldTileset.tsx"
        }],
    "tilewidth":16,
    "type":"map",
    "version":1.2,
    "width":24
}
"""