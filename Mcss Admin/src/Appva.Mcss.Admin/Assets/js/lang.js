﻿var validation = {
    nameRequired: "Namn måste fyllas i.",
    firstNameRequired: "名前は記入しなければなりません",
    lastNameRequired: "姓名は記入しなければなりません",
    addressRequired: "住所を選択しなければならない",
    scheduleRequired: "リストを選択しなければなりません",
    scheduleRemote: "このリストはもう以前にインプットされています",
    dateRequired: "日付を記入しなければなりません",
    dateFormat: "日付は８つの数字をハイフォンで結ぶ形で記入しなければならない。　例　２０１２－１２－２１",
    dateLessThan: "開始日は終了日よりも以前の日時でなければならない",
    dateGreaterThan: "終了日は開始日よりも後の日にちでなければならない",
    taskNameRequired: "支援は指摘されなければならない",
    taskIntervalRequired: "En frekvens måste anges.",
    taskTimesRequired: "Klockslag måste anges.",
    taskRangeRequired: "Ges inom måste fyllas i.",
    betweenZeroAndHoundrerThousand: "０－９９９９９の間でなければならない",
    taskInventoryRequired: "Saldo måste väljas",
    calendarIntervalToShort: "Upprepnings-intervallet kan inte vara kortare än aktiviten",
    calendarCategoryRequired: "Kategori måste väljas",
    calendarNewCategoryNameRequired: "Namn måste anges på ny kategori",
    calendarDescriptionRequired: "Aktiviteten måste ha en beskrivning.",
    dateLessThanOrEqual: "開始日は終了日よりも以前の日時でなければならない",
    dateGreaterThanOrEqual: "終了日は開始日よりも後の日にちでなければならない",
    timeRequired: "Tid måste fyllas i.",
    timeFormat: "Tid måste fyllas i med fyra siffror och kolon, t.ex. 14:30.",
    socialSecurityNumberRequired: "マイナンバーを記入しなければなりません",
    socialSecuityNumberDuplicate: "マイナンバーは、すでにMCSSのラインに記入されています",
    socialSecurityFormat: "マイナンバーは１２の数字をハイフォンで結んだものでなければなりません。例　１９０１０１０１－０００１",
    mailRequired: "E-postadress måste fyllas i.",
    mailFormat: "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se.",
    passwordRequired: "コードを記入しなければなりません",
    passwordFormat: "Ange ett fyrsiffrigt lösenord.",
    passwordMaxLength: "Maximalt antal tecken är 7 siffror.",
    passwordMinLength: "Minimilängden är fyra siffror.",
    passwordRemote: "Lösenordet används redan av en annan medarbetare.",
    titleRequired: "タイトルを選択しなければなりません",
    delegationRequired: "Delegering måste väljas.",
    delegationTypeRequired: "Delegeringstyp måste väljas.",
    patientRequired: "Boende måste väljas.",
    valueRequired: "Mängd måste fyllas i.",
    minValueGreaterThanZero: "Mängd måste vara ett numeriskt värde och vara större än 0.",
    maxValueLessThanHoundredThousand: "Mängd får ej vara större än 99999.",
    knowledgeTestRequired: "Kunskapstest måste fyllas i."
}

var language = language || { };

language.chart = {
    loading: "Loading.",
    problemOccured: "間違いがあった　",
    iTid: "決められた時間にされた"
};

language.date = {
    jan: "1月",
    feb: "2月",
    mar: "3月",
    apr: "4月",
    maj: "5月",
    jun: "6月",
    jul: "7月",
    aug: "8月",
    sep: "9月",
    okt: "10月",
    nov: "11月",
    dec: "12月",
    januari: "1月",
    februari: "2月",
    mars: "3月",
    april: "4月",
    maj: "5月",
    juni: "6月",
    juli: "7月",
    augusti: "8月",
    september: "9月",
    oktober: "10月",
    november: "11月",
    december: "12月",
}

language.general = {
    hideLabel: "Dölj",
    showLabel: "Visa",
    checkIfDeviationShouldBeReported: "Kontrollera om avvikelsen ska journalföras.",
    checkIfAllDeviationsShouldBeReported: "Alla avvikelser för patienten kommer att kvitteras. Kontrollera om någon avvikelse ska journalföras.",
    removeActivity: "Vill du fortsätta ta bort aktiviteten?",
    removeActivityWithAbsence: "Pausade ordinationer kommer att återupptas. Vill du fortsätta ta bort frånvaron?",
    removeLabel: "除去する",
    removeList: "リストを除去する",
    removeListPrompt: "Vill du verkligen ta bort",
    cancelLabel: "中止する",
    removeRowPrompt: "Vill du verkligen ta bort raden?",
    removeRow: "Ta bort raden",
    removeKnowledgeTest: "Vill du verkligen ta bort kunskapstestet?",
    removeUserPrompt: "Vill du verkligen ta bort personen?",
    removeUser: "Ta bort personen"
}

language.passwordStrength = {
    label: "L&ouml;senordets styrka:",
    veryLow: "mycket l&aring;g",
    low: "l&aring;g",
    medium: "medel",
    strong: "h&ouml;g",
    veryStrong: "mycket h&ouml;g",
}