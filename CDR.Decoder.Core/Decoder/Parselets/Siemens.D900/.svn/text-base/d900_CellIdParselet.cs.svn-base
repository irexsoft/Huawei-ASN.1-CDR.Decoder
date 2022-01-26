using System;

namespace CDR.Decoder.Parselets
{
    class d900_CellIdParselet : GenericParselet
    {
        #region Summary
        /*
         * coding a:
         * +-------------------------------------------------------------------+
         * ¦ cellId                         ¦                                  ¦
         * +--------------------------------+----------------------------------¦
         * ¦ content                        ¦ meaning                          ¦
         * +--------------------------------+----------------------------------¦
         * ¦  internal structure:           ¦                                  ¦
         * ¦             +--------------+   ¦ M1M2M3 Mobile Country Code (MCC) ¦
         * ¦    octet 1  ¦ M2       M1  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦ N1N2N3 Mobile Network Code (MNC) ¦
         * ¦          2  ¦ N3(*)    M3  ¦   ¦ LAC    Location Area Code        ¦
         * ¦             +--------------¦   ¦ CI     Cell Identification       ¦
         * ¦          3  ¦ N2(*)    N1  ¦   ¦ (*)    if not used: filled       ¦
         * ¦             +--------------¦   ¦        with value B'1111         ¦
         * ¦          4  ¦   LAC (lsb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          5  ¦   LAC (msb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          6  ¦    CI (lsb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          7  ¦    CI (msb)  ¦   ¦                                  ¦
         * ¦             +--------------+   ¦                                  ¦
         * +-------------------------------------------------------------------+
         * The cell identity from the start of the call is always entered in this field.
         * MCC is coded as TBCD string (octets 1, 2). The default value is H'FFF'.
         * MNC is coded as TBCD string (octet 2, 3). The default value is H'FFF'.
         * LAC and CI are binary coded as shown above. The default value for octets 4..7 is H'00'.
         * 
         * coding b:
         * +-------------------------------------------------------------------+
         * ¦ cellId                         ¦                                  ¦
         * +--------------------------------+----------------------------------¦
         * ¦ content                        ¦ meaning                          ¦
         * +--------------------------------+----------------------------------¦
         * ¦  internal structure:           ¦                                  ¦
         * ¦             +--------------+   ¦                                  ¦
         * ¦    octet 1  ¦    CI (msb)  ¦   ¦ CI     Cell Identification       ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          2  ¦    CI (lsb)  ¦   ¦                                  ¦
         * ¦             +--------------+   ¦                                  ¦
         * +-------------------------------------------------------------------+
         * The cell identity from the start of the call is always entered in this field.
         * The default value is H'0000'.
         * 
         * coding d:
         * +-------------------------------------------------------------------+
         * ¦ cellId                         ¦                                  ¦
         * +--------------------------------+----------------------------------¦
         * ¦ content                        ¦ meaning                          ¦
         * +--------------------------------+----------------------------------¦
         * ¦  internal structure:           ¦                                  ¦
         * ¦             +--------------+   ¦ M1M2M3 Mobile Country Code (MCC) ¦
         * ¦    octet 1  ¦ M2       M1  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦ N1N2N3 Mobile Network Code (MNC) ¦
         * ¦          2  ¦ N3(*)    M3  ¦   ¦ LAC    Location Area Code        ¦
         * ¦             +--------------¦   ¦ CI     Cell Identification       ¦
         * ¦          3  ¦ N2(*)    N1  ¦   ¦ (*)    if not used: filled       ¦
         * ¦             +--------------¦   ¦        with value B'1111         ¦
         * ¦          4  ¦   LAC (msb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          5  ¦   LAC (lsb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          6  ¦    CI (msb)  ¦   ¦                                  ¦
         * ¦             +--------------¦   ¦                                  ¦
         * ¦          7  ¦    CI (lsb)  ¦   ¦                                  ¦
         * ¦             +--------------+   ¦                                  ¦
         * +-------------------------------------------------------------------+
         * The cell identity from the start of the call is always entered in this field.
         * MCC is coded as TBCD string (octets 1, 2). The default value is H'FFF'.
         * MNC is coded as TBCD string (octet 2, 3). The default value is H'FFF'.
         * LAC and CI are binary coded as shown above. The default value for octets 4..7 is H'00'.
         */

        /*
         * This is a list of Mobile Country Codes (MCCs) defined in ITU E.212 ("Land Mobile Numbering Plan") for use in identifying mobile stations in wireless telephone networks, particularly GSM and UMTS networks. An MCC is often used in combination with a Mobile Network Code (as a "MCC / MNC tuple") in order to uniquely identify a network operator.
         * Code (MCC)  Country
         * 202 	Greece
         * 204 	Netherlands
         * 206 	Belgium
         * 208 	France
         * 212 	Monaco
         * 213 	Andorra
         * 214 	Spain
         * 216 	Hungary
         * 218 	Bosnia and Herzegovina
         * 219 	Croatia
         * 220 	Serbia (Republic of)
         * 222 	Italy
         * 225 	Vatican City State
         * 226 	Romania
         * 228 	Switzerland
         * 230 	Czech Republic
         * 231 	Slovakia
         * 232 	Austria
         * 234 	United Kingdom
         * 235 	United Kingdom
         * 238 	Denmark
         * 240 	Sweden
         * 242 	Norway
         * 244 	Finland
         * 246 	Lithuania
         * 247 	Latvia
         * 248 	Estonia
         * 250 	Russian Federation
         * 255 	Ukraine
         * 257 	Belarus
         * 259 	Moldova
         * 260 	Poland
         * 262 	Germany
         * 266 	Gibraltar (UK)
         * 268 	Portugal
         * 270 	Luxembourg
         * 272 	Ireland
         * 274 	Iceland
         * 276 	Albania
         * 278 	Malta
         * 280 	Cyprus
         * 282 	Georgia
         * 283 	Armenia
         * 284 	Bulgaria
         * 286 	Turkey
         * 288 	Faroe Islands (Denmark)
         * 289 	Abkhazia (Georgia)
         * 290 	Greenland (Denmark)
         * 292 	San Marino
         * 293 	Slovenia
         * 294 	Republic of Macedonia
         * 295 	Liechtenstein
         * 297 	Montenegro (Republic of)
         * 302 	Canada
         * 308 	Saint Pierre and Miquelon (France)
         * 310 	United States of America
         * 311 	United States of America
         * 312 	United States of America
         * 313 	United States of America
         * 314 	United States of America
         * 315 	United States of America
         * 316 	United States of America
         * 330 	Puerto Rico (US)
         * 332 	United States Virgin Islands (US)
         * 334 	Mexico
         * 338 	Jamaica
         * 340 	Guadeloupe (France)
         * 340 	Martinique (France)
         * 342 	Barbados
         * 344 	Antigua and Barbuda
         * 346 	Cayman Islands (UK)
         * 348 	British Virgin Islands (UK)
         * 350 	Bermuda (UK)
         * 352 	Grenada
         * 354 	Montserrat (UK)
         * 356 	Saint Kitts and Nevis
         * 358 	Saint Lucia
         * 360 	Saint Vincent and the Grenadines
         * 362 	Netherlands Antilles (Netherlands)
         * 363 	Aruba (Netherlands)
         * 364 	Bahamas
         * 365 	Anguilla
         * 366 	Dominica
         * 368 	Cuba
         * 370 	Dominican Republic
         * 372 	Haiti
         * 374 	Trinidad and Tobago
         * 376 	Turks and Caicos Islands (UK)
         * 400 	Azerbaijani Republic
         * 401 	Kazakhstan
         * 402 	Bhutan
         * 404 	India
         * 405 	India
         * 410 	Pakistan
         * 412 	Afghanistan
         * 413 	Sri Lanka
         * 414 	Myanmar
         * 415 	Lebanon
         * 416 	Jordan
         * 417 	Syria
         * 418 	Iraq
         * 419 	Kuwait
         * 420 	Saudi Arabia
         * 421 	Yemen
         * 422 	Oman
         * 423 	Palestine
         * 424 	United Arab Emirates
         * 425 	Israel
         * 426 	Bahrain
         * 427 	Qatar
         * 428 	Mongolia
         * 429 	Nepal
         * 430 	United Arab Emirates (Abu Dhabi)
         * 431 	United Arab Emirates (Dubai)
         * 432 	Iran
         * 434 	Uzbekistan
         * 436 	Tajikistan
         * 437 	Kyrgyz Republic
         * 438 	Turkmenistan
         * 440 	Japan
         * 441 	Japan
         * 450 	Korea, South
         * 452 	Viet Nam
         * 454 	Hong Kong (PRC)
         * 455 	Macau (PRC)
         * 456 	Cambodia
         * 457 	Laos
         * 460 	China
         * 466 	Taiwan
         * 467 	Korea, North
         * 470 	Bangladesh
         * 472 	Maldives
         * 502 	Malaysia
         * 505 	Australia
         * 510 	Indonesia
         * 514 	East Timor
         * 515 	Philippines
         * 520 	Thailand
         * 525 	Singapore
         * 528 	Brunei Darussalam
         * 530 	New Zealand
         * 534 	Northern Mariana Islands (US)
         * 535 	Guam (US)
         * 536 	Nauru
         * 537 	Papua New Guinea
         * 539 	Tonga
         * 540 	Solomon Islands
         * 541 	Vanuatu
         * 542 	Fiji
         * 543 	Wallis and Futuna (France)
         * 544 	American Samoa (US)
         * 545 	Kiribati
         * 546 	New Caledonia (France)
         * 547 	French Polynesia (France)
         * 548 	Cook Islands (NZ)
         * 549 	Samoa
         * 550 	Federated States of Micronesia
         * 551 	Marshall Islands
         * 552 	Palau
         * 602 	Egypt
         * 603 	Algeria
         * 604 	Morocco
         * 605 	Tunisia
         * 606 	Libya
         * 607 	Gambia
         * 608 	Senegal
         * 609 	Mauritania
         * 610 	Mali
         * 611 	Guinea
         * 612 	Côte d'Ivoire
         * 613 	Burkina Faso
         * 614 	Niger
         * 615 	Togolese Republic
         * 616 	Benin
         * 617 	Mauritius
         * 618 	Liberia
         * 619 	Sierra Leone
         * 620 	Ghana
         * 621 	Nigeria
         * 622 	Chad
         * 623 	Central African Republic
         * 624 	Cameroon
         * 625 	Cape Verde
         * 626 	São Tomé and Príncipe
         * 627 	Equatorial Guinea
         * 628 	Gabonese Republic
         * 629 	Republic of the Congo
         * 630 	Democratic Republic of the Congo
         * 631 	Angola
         * 632 	Guinea-Bissau
         * 633 	Seychelles
         * 634 	Sudan
         * 635 	Rwandese Republic
         * 636 	Ethiopia
         * 637 	Somalia
         * 638 	Djibouti
         * 639 	Kenya
         * 640 	Tanzania
         * 641 	Uganda
         * 642 	Burundi
         * 643 	Mozambique
         * 645 	Zambia
         * 646 	Madagascar
         * 647 	Réunion (France)
         * 648 	Zimbabwe
         * 649 	Namibia
         * 650 	Malawi
         * 651 	Lesotho
         * 652 	Botswana
         * 653 	Swaziland
         * 654 	Comoros
         * 655 	South Africa
         * 657 	Eritrea
         * 702 	Belize
         * 704 	Guatemala
         * 706 	El Salvador
         * 708 	Honduras
         * 710 	Nicaragua
         * 712 	Costa Rica
         * 714 	Panama
         * 716 	Perú
         * 722 	Argentine Republic
         * 724 	Brazil
         * 730 	Chile
         * 732 	Colombia
         * 734 	Venezuela
         * 736 	Bolivia
         * 738 	Guyana
         * 740 	Ecuador
         * 742 	French Guiana (France)
         * 744 	Paraguay
         * 746 	Suriname
         * 748 	Uruguay
         * 
         */

        /* 
         * MOBILE  NETWORK  CODE (MNC)  FOR 
         * THE INTERNATIONAL  IDENTIFICATION 
         * PLAN  FOR  MOBILE  TERMINALS  AND 
         * MOBILE  USERS  (ACCORDING  TO 
         * ITU-T  RECOMMENDATION E.212 (05/2004))
         *  
         * (POSITION  ON  1  FEBRUARY  2008) 
         * 
         * Russian Federation   
         *   Mobile Telesystems                        250  01 
         *   Megafon                                   250  02 
         *   Nizhegorodskaya Cellular Communications   250  03 
         *   Sibchallenge                              250  04 
         *   Mobile Comms System                       250  05 
         *   BM Telecom                                250  07 
         *   Don Telecom                               250  10 
         *   Orensot                                   250  11 
         *   Baykal Westcom                            250  12 
         *   Kuban GSM                                 250  13 
         *   New Telephone Company                     250  16 
         *   Ermak RMS                                 250  17 
         *   Volgograd Mobile                          250  19 
         *   ECC                                       250  20 
         *   Extel                                     250  28 
         *   Uralsvyazinform                           250  39 
         *   Stuvtelesot                               250  44 
         *   Printelefone                              250  92 
         *   Telecom XXI                               250  93 
         *   Bec Line GSM                              250  99
         * 
         */

        /*
         * 
         * Siemens D900: Using little-endian coding (coding a)
         * 
         */

        #endregion

        public d900_CellIdParselet()
            : base()
        {
            RegisterMethod("CellId", ValueAsCellId);
            RegisterMethod("MCC", ValueAsMCC);
            RegisterMethod("MNC", ValueAsMNC);
            RegisterMethod("LAC", ValueAsLAC);
            DefaultValueType = "CellId";
        }

        public static string ValueAsMCC(byte[] value)
        {
            if ((value == null) || (value.Length < 7))
            {
                return String.Empty;
            }
            else
            {
                return ((Int16)(((value[0] & 0xF) << 8) | (value[0] & 0xF0) | (value[1] & 0xF))).ToString("X");
            }
        }

        public static string ValueAsMNC(byte[] value)
        {
            if ((value == null) || (value.Length < 7))
            {
                return String.Empty;
            }
            else
            {
                Int16 mnc = (Int16)(((value[1] & 0xF0) >> 4) | (value[2] & 0xF0) | ((value[2] & 0xF) << 8));
                if ((mnc & 0xF) == 0xF)
                {
                    mnc >>= 4;
                    if ((mnc & 0xF) == 0xF)
                    {
                        mnc >>= 4;
                    }
                }
                return mnc.ToString("X3");
            }
        }

        public static string ValueAsLAC(byte[] value)
        {
            if (value == null)
            {
                return String.Empty;
            }
            else if (value.Length == 7)
            {
                // Octets 1-3 contains MCC + MNC
                return ((Int16)((value[4] << 8) | value[3])).ToString();
            }
            else if (value.Length == 4)
            {
                return ((Int16)((value[0] << 8) | value[1])).ToString();
            }
            return String.Empty;
        }

        public static string ValueAsCellId(byte[] value)
        {
            if (value == null)
            {
                return String.Empty;
            }
            else if (value.Length == 7)
            {
                // Octets 1-3 contains MCC + MNC
                return ((Int16)((value[6] << 8) | value[5])).ToString();
            }
            else if (value.Length == 4)
            {
                return ((Int16)((value[2] << 8) | value[3])).ToString();
            }
            return String.Empty;
        }
    }
}
