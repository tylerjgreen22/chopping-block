// Class that creates an object representing post parameters that can be sent to the back end to modify results
export class PostParams {
  categoryId = '';
  sort = 'name';
  pageIndex = 1;
  pageSize = 6;
  search = '';
  userId = '';
  byUser = false;
}
