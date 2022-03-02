const getInfo = ({firstName, lastName, age}) => `${firstName} ${lastName}. Age: ${age}`;
const person1 = {firstName: 'Son Tung', lastName: 'MTP', age: 25};
console.log(getInfo(person1));
// Son Tung MTP. Age: 25
console.log(getInfo({firstName:'Huan', lastName:'Rose'}));
// Huan Rose. Age: undefined
const person2 = {firstName: 'Son Tung', lastName: 'MTP', age: 25};
console.log(person1 === person2);
//false
const setPersonName = (person, name) => {
    person.name = name;
};
setPersonName(person1, 'Tung Nui');
console.log(getInfo(person1));
console.log(getInfo({...person2, name: "Tung Nui"}));
// Son Tung MTP. Age: 25
// Son Tung MTP. Age: 25

// Bai2
const students = [
    { name:"Alex", grade: 15, point : 15},
    { name:"Devlin", grade: 15, point : 25},
    { name:"Eagle", grade: 13, point : 12},
    { name:"Sam", grade: 14, point : 26},
]

// let sortByName = students.sort((a, b) => a.name.localeCompare(b.name));

// let sortByGradeIncrease = students.sort(function (a, b) {return a.grade - b.grade})

// let sortByGradeDecrease = students.sort(function (a, b) {return b.grade - a.grade})
// console.log(sortByName);
// console.log(sortByGradeIncrease);
// console.log(sortByGradeDecrease);
// console.log(students.filter(x => x.point > 15));
// let gradeEqual15Students = students.filter(x => x.grade == 15);
// let totalPoint = gradeEqual15Students.reduce(function(sum, record){
//     return sum + record.point;
// },0);
// let total = 0
// let alternativeTotalPoint = gradeEqual15Students.forEach(function(record){
//     total += record.point;
// })
// console.log(totalPoint);
// console.log(total);
console.log(students.pop());